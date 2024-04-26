Feature: Product
	Test the product page functionalities

@Teardown.API.DeleteCreatedProduct
Scenario: Create product and verify the details
	When I open product menu
	And I click create new product
	And I fill product fields with following details
		| Name       | Description        | Price | ProductType |
		| Headphones | Noise cancellation | 300   | PERIPHARALS |
	And I click create button
	And I click the Details link of the newly created product
	Then I see all the product details are created as expected

@Setup.API.CreateProduct 
	@ProductName:Monitor
	@Description:HD_monitor
	@Price:400
	@ProductType:MONITOR
@Teardown.API.DeleteCreatedProduct
Scenario: Edit Product and verify if its updated
	When I open product menu
	And I click the Edit link of the newly created product
	And I fill product fields with following details
		| Name    | Description           | Price | ProductType |
		| Monitor | HD Resolution monitor | 500   | MONITOR     |
	And I click save button
	And I click the Details link of the newly created product
	Then I see all the product details are created as expected
