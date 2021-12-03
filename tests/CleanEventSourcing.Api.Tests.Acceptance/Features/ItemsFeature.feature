Feature: ItemsFeature
Item management for CRUD actions

    @Acceptance
    Scenario: A user cannot create an item when providing an empty description
        When I create a new item ""
        Then the creation response should return a "400" status code

    @Acceptance
    Scenario: A user successfully creates an item
        When I create a new item "item 1"
        Then the creation response should return a "201" status code
        And the creation response contains location header for retrieving the item
        
    @Acceptance
    Scenario: A user cannot retrieve an item when providing an empty guid
        When I retrieve the item using an empty id
        Then the retrieval response should return a "400" status code
        