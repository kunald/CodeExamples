Feature: Add Part
Allow users to create and store new part
As long as the new part has PartNumber, PartDescription,UnitOfMeasure and SalesLeadTime

Scenario: HappyPath
Given a user has entered information about a Part
Then that Part should be stored in the system

When user user change the Description
Then the description of that part should be updated

When the user change the UnitOfMeasure
Then the UnitOfMeasure of that part should be update

