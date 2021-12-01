Feature: ItemsFeature
Item management for CRUD actions

    @Acceptance
    Scenario: A user cannot create an item when providing an empty description
        When I create a new item ""
        Then the creation response should return a "400" status code