Feature: ItemsFeature
Item management for CRUD actions

    @Acceptance
    Scenario: A user cannot create an item when providing an empty description
        When a user creates a new item ""
        Then the creation response should return a "400" status code

    @Acceptance
    Scenario: A user successfully creates an item
        When a user creates a new item "item 1"
        Then the creation response should return a "201" status code
        And the creation response contains location header for retrieving the item

    @Acceptance
    Scenario: A user cannot retrieve an item when providing an empty guid
        When I retrieve the item using an empty id
        Then the retrieval response should return a "400" status code

    @Acceptance
    Scenario: A user successfully retrieves an item after creating it
        Given a user creates a new item "item 1"
        When a user gets the created item using the location header
        Then the retrieved item should have the description "item 1"
        And the retrieval response should return a "200" status code

    @Acceptance
    Scenario: A user cannot update an item when providing an empty id
        Given a user creates a new item "item 1"
        When a user updates the created item with an empty id
        Then the update response should return a "400" status code

    @Acceptance
    Scenario: A user cannot update an item when providing an empty description
        Given a user creates a new item "item 1"
        When a user updates the created item with the description ""
        Then the update response should return a "400" status code

    @Acceptance
    Scenario: A user successfully updates an item after creating it
        Given a user creates a new item "item 1"
        When a user updates the created item with the description "item 2"
        And a user gets the created item using the location header
        Then the retrieval response should return a "200" status code
        And the update response should return a "204" status code
        And the retrieved item should have the description "item 2"