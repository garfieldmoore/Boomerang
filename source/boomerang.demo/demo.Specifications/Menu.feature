Feature: SpecFlowFeature1
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Show coffee menu
	Given A cafe only serves 'Mocha' with description 'Coffee with chocolate'
	When bobbie asks for the menu
	Then bobbie sees the menu
	| Name  | Description           |
	| Mocha | Coffee with chocolate |
