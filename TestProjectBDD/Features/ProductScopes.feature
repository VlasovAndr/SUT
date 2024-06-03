Feature: ProductScopes
	Test the product page functionalities using different scopes

@UI_Steps
@Teardown.UI.DeleteCreatedProduct
Scenario: Create product and verify the details (UI Scope)
	When I create product with following details
		| Name                    | Description        | Price | ProductType |
		| Headphones_UIScope_Item | Noise cancellation | 300   | PERIPHARALS |
	Then I validate all the product details are created as expected

@UI_Steps
@Setup.UI.CreateProduct 
	@ProductName:Mouse_UIScope_Item
	@Description:MouseDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.UI.DeleteCreatedProduct
Scenario: Edit Product and verify if its updated (UI Scope)
	When I edit newly created product with following details
		| Name                    | Description            | Price | ProductType |
		| Mouse_UIScope_Item Edit | Mouse Description Edit | 300   | PERIPHARALS |
	Then I validate all the product details are created as expected

@UI_Steps
@Setup.API.CreateProduct 
	@ProductName:Monitor_UIScope_Item_Delete
	@Description:MonitorDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.API.DeleteCreatedProduct
Scenario: Delete Product and verify it (UI Scope)
	When I delete newly created product
	Then I validate product is removed from the table


@API_Steps
@Teardown.API.DeleteCreatedProduct
Scenario: Create product and verify the details (API Scope)
	When I create product with following details
		| Name                     | Description        | Price | ProductType |
		| Headphones_APIScope_Item | Noise cancellation | 300   | PERIPHARALS |
	Then I validate all the product details are created as expected

@API_Steps
@Setup.API.CreateProduct 
	@ProductName:Mouse_APIScope_Item
	@Description:MouseDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.API.DeleteCreatedProduct
Scenario: Edit Product and verify if its updated (API Scope)
	When I edit newly created product with following details
		| Name                     | Description            | Price | ProductType |
		| Mouse_APIScope_Item Edit | Mouse Description Edit | 300   | PERIPHARALS |
	Then I validate all the product details are created as expected

@API_Steps
@Setup.API.CreateProduct 
	@ProductName:Monitor_APIScope_Item_Delete
	@Description:MonitorDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.API.DeleteCreatedProduct
Scenario: Delete Product and verify it (API Scope)
	When I delete newly created product
	Then I validate product is removed from the table


@Database_Steps
@Teardown.Db.DeleteCreatedProduct
Scenario: Create product and verify the details (Databse Scope)
	When I create product with following details
		| Name                     | Description        | Price | ProductType |
		| Headphones_DbScope_Item | Noise cancellation | 300   | PERIPHARALS |
	Then I validate all the product details are created as expected

@Database_Steps
@Setup.Db.CreateProduct 
	@ProductName:Mouse_DbScope_Item
	@Description:MouseDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.Db.DeleteCreatedProduct
Scenario: Edit Product and verify if its updated (Databse Scope)
	When I edit newly created product with following details
		| Name               | Description            | Price | ProductType |
		| Mouse_DbScope_Item | Mouse Description Edit | 300   | PERIPHARALS |
	Then I validate all the product details are created as expected

@Database_Steps
@Setup.Db.CreateProduct 
	@ProductName:Monitor_DbScope_Item_Delete
	@Description:MonitorDescription
	@Price:400
	@ProductType:MONITOR
@Teardown.Db.DeleteCreatedProduct
Scenario: Delete Product and verify it (Databse Scope)
	When I delete newly created product
	Then I validate product is removed from the table