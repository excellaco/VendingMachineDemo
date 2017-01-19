Feature: ReleaseChange
	In order to not donate all of my money to the vending machine company
	As a paying customer
	I want to receive change back that isn't used to purchase a product.

Scenario: No change back when none given
	Given I have not inserted a quarter
	When I release the change
	Then I should receive no change

# TODO: Change back when I release change without purchasing a product

# TODO: Remaining change back when I don't use it all on a product