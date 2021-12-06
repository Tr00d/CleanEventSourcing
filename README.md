# CleanEventSourcing

## Summary
This project came up as a personal challenge. This is a simple ToDoList api with CRUD and Event Sourcing like we can find many on GitHub.


Still, every single implementation I saw were using either Reflection or Switches (sometimes Dictionary but the problem remains the same) to construct an aggregate based on previous events.
I wanted to try a clean**er** approach.

My proposal comes with a compromise on the workflow: an event applies its logic on an aggregate. 
This is not really a bad compromise as it would allow us to creates more events without affecting the aggregate (OCP).
Keeping that in mind, I kept the logic in the Aggregate.Apply method for encapsulation reasons: an event calls the aggregate when passing himself as a parameter.

I hope you'll find this implementation interesting. 


