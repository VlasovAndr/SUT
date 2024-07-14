Feature: Product
	Test the product page functionalities

@UI_Steps
@Teardown.API.DeleteCreatedProduct
Scenario: Create product and verify the details
	When I open product menu
	And I click create new product
	And I fill product fields with following details
		| Name             | Description          | Price        | ProductType |
		| Keyboard_UI_Item | Keyboard Description | invalidValue | PERIPHARALS |
	And I click create button
	And I click the Details link of the newly created product
	Then I see all the product details are created as expected

@UI_Steps
@Setup.API.CreateProduct 
	@ProductName:Monitor_UI_Item
	@Description:MonitorDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.API.DeleteCreatedProduct
Scenario: Edit Product and verify if its updated
	When I open product menu
	And I click the Edit link of the newly created product
	And I fill product fields with following details
		| Name                 | Description              | Price | ProductType |
		| Monitor_UI_Item Edit | Monitor Description Edit | 10000 | MONITOR     |
	And I click save button
	And I click the Details link of the newly created product
	Then I see all the product details are created as expected

@UI_Steps
@Setup.API.CreateProduct 
	@ProductName:Monitor_UI_Item_Delete
	@Description:MonitorDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.API.DeleteCreatedProduct
Scenario: Delete Product and verify it
	When I open product menu
	And I click the Delete link of the newly created product
	And I click delete button
	Then I validate product is removed from the table