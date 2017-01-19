Feature: ReleaseChange
	In order to not donate all of my money to the vending machine company
	As a paying customer
	I want to receive change back that isn't used to purchase a product.

Scenario: No change back when none given
	Given I have not inserted a quarter
	When I release the change
	Then I should receive no change

Scenario: Get Change Back When not purchasing a product
	Given I have inserted a quarter
	When I release the change
	Then I should receive a quarter

Scenario: Get all my change back when not purchasing
	Given I have inserted a quarter
	And I have inserted a quarter
	And I have inserted a quarter
	When I release the change
	Then I should receive 75 cents



# TODO: Remaining change back when I don't use it all on a product