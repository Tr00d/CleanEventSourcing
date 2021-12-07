# CleanEventSourcing

Here's the rating on BetterCodeHub. The rating is 9/10 because everything is one assembly but being a kata, it's not really an issue.
[![BCH compliance](https://bettercodehub.com/edge/badge/Tr00d/CleanEventSourcing?branch=main)](https://bettercodehub.com/)

## Summary

This project came up as a personal challenge. This is a simple ToDoList Rest Api with CRUD and Event Sourcing like many others on GitHub. This is **not** a production-ready solution as data stores are both in-memory but just a fun
personal project.

## The "problem"

Still, every single implementation I saw were using either Reflection or 'switch' (sometimes Dictionary<IEvent, Action>
but the problem remains the same) to construct an aggregate based on previous events. I wanted to try a clean**er**
approach. I do not pretend my implementation is perfect, I was just able to remove what I consider being "code smells".

Here are a couple of remediation for those issues :

#### No switch when creating aggregate
```csharp
    private static T CreateAggregate(IEnumerable<Option<IIntegrationEvent<T>>> events)
    {
        T aggregate = new();
        events
            .ToList()
            .ForEach(listItem => listItem.IfSome(value => ApplyEvent(aggregate, value)));
        return aggregate;
    }

    private static void ApplyEvent(T aggregate, IIntegrationEvent<T> integrationEvent) => integrationEvent.Apply(aggregate);

    public interface IIntegrationEvent<T> : IIntegrationEvent
        where T : IAggregate
    {
        void Apply(Option<T> aggregate);
    }
```
#### No switch when projecting changes
```csharp
    public class InMemoryReadService : IReadService, INotificationHandler<CreatedItemEvent>,
        INotificationHandler<UpdatedItemEvent>, INotificationHandler<DeletedItemEvent>
    {
    }
```
#### No inheritance on aggregates - Composition instead
```csharp
    public interface IAggregate
    {
        Guid Id { get; }
        Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents();
    }

    public class Item : IAggregate
    {
        private readonly AggregateBase baseAggregate = new();
    }
```
#### No inheritance on events - Composition instead
```csharp
    public class CreatedItemEvent : IIntegrationEvent<Item>
    {
        private readonly ItemBaseEvent baseEvent = new();
    }
```
## Trade-offs

My proposal comes with a compromise on the workflow: an event applies its logic on an aggregate. This is not really a
bad compromise as it would allow us to creates more events without affecting the aggregate (OCP). Keeping that in mind,
I kept the logic in the Aggregate.Apply method for encapsulation reasons: an event calls the aggregate when passing
himself as a parameter.

## Conclusion

Thanks for reading, I hope you'll find this implementation interesting.

Don't hesitate to provide feedbacks.


