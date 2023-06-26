# ShopOnlineSolution 

- ShopOnlineSolution is part of a Blazor Shopping Cart Application video tutorial demonstrated on YouTube at this location,


# projects

- the first project is blazor wasm shop online solution.
- and the second project is shop online REST api.(Asp.net core web API). and choose the authentication to none. enable the open api (swagger ui)
- 
# Entity Relationship

- the user entity has one to one relationship with the cart 
- the cart entity has one to many relationship with the cart Item
- the product entity has one to many relationship with the cart Item
- the Porduct category entity has one to many relationship with the product entity.

# DB 

- for this project im gon use the SQL server, and to connect our entity to the database, we can install couple of nuget packages. in the api 
- 1. entity framework core sql server
- 2. entity framework core .tools
- in order to seed the db appropriately, we can override the method that exist in the dbContext base class 
- and the method is on model creation
- for the migration open the nuget package mgr and add the command ```Add-Migratino InitialCreate``` and make sure the api project is the start up project
- this will creates the Create.cs this code contains the up()create our table and also contains the seeds to our database table. and the down method 
- and to run the migration ```update-database```
- and we can see the database has been created in our sql server with the table.
- if we re not happy with the migration we can undo the changes by using the ```update-database 0``` 0 - > the state before the migration, and all the migrations will be rolled back.
- ```remove-migrations``` to remove the migration, will remove the last migration that has not yet run.
- if we ve multiple migrations then we wana roll back to the particular migration, then ```update-database Migration2```


# part 2 

- in this we will retrieve the data from our database
- and returnig the data to the client blazor component.
- the code for this functionality will be implemented in the web api component.
- we ve to create the classes that represent the data passed b/w the web api component and the blazor component.(DTO)
- as we know when  we pass the data b/w the api to the web comp we don't necessarily need to pass the exact data obj(entity). 
- its always good to use the DTO instead.
- create a new project "ShopOnlineModels" (of slolution type class library) 
- and in there we can create our Dtos
- we re gon use the repository design pattern to abstract our data handling layer
- repository are classes/components that encapsulates the logic required to access data sources
- we can use repository to centralize the common data access functionality.
- which facilitates better unit testing, efficiency and cleaner code.
- /respositories/contracts/IProductRepository.cs"
- then in the repository/ProductRepository.cs" we can impl this interface(and the method)
- and inside the api project we ve to create the controller class (Api controller template) "ProductController.cs" there we can make our endpoints.
- In our shoponline.models project add a project reference, coz this is where our product Dto resides 
- we will be using the swagger ui to run and test our action methods.
  

# part 3
-  in our Blazor web client to call the action method that we implemented and display the data to the client
-  the Services/dir cs classes will wrap the functionality to interact with our api
-  services/contracts/IProductService.cs" 
-  and add the reference to our shoponline.models project (so we can access to the dtos)
-  in the productBase.razor/pages, we overrides the lifecycle events onInitializedAsync()
-  the razor comp process lifecycle events in set of synchronous and asynchronous lifecycle methods, 
-  the lifecycle methods can be overridden to perform additional operations during comp initialization and rendering.
-  if we made any type with the obj name or the file/dir, we can fix the migration simply by the following method.
-  in our shoponline.api, find the shoponlineDBContext.cs in the onModelCreate() replace/fix the typo.
-  and in our package mgr ```add-migration fix_spelling```  to add the migration
-  and ```update-database``` to apply the migrations.
-  to start our client project and the api project at the same time in the main solution -> select multiple start projects 
-  since our blazor server has diff url than our api comp we may experience the cors error for the first time. 
-  so for our blazor comp to access the relavant resources add useCors() in the program.cs (in our api comp to accept the cors) and impl the policy with the origin 


# Part 4 

- display data of the specific project to the user 
- and provide a addto card button to add the product to the shopping cart.
- create a new comp in the pages -> productDetails component(holds  the details of our product)
- shopping cart component contains the products that we added in the cart and displays them for the checkout

# Remove 

- implement the remove functionality for the user to remove the product from the shopping cart.
- create a delete item() in the repository. which will remove the product from the db table.
- then create an endpt in the client component.

# update the quantity(shopping cart)

- update qty button when the user wants to change quantity.
- in order to implement the update functionality, we can use the js code.
- the feature to interact with the js code in our blazor with the js interoperability with blazor
- to access the js code in our blazor with the js, we can use the build in js type runtime. "IJSRuntime" inject this in our blazor comp(shopping cart service).
- now we ve to enable the navigation for the user to nav anywhere in the application.
- and then show them how many items were added in the cart with the cart item that displays the quantity of the items that were added.
- we will use the cs events to update/ faciliate the communication of data b/w the loosely coupled blazor components. event Action<int> onlineShopCartChanged;
- nShoppingCartChanged.Invoke(totalQty); //then raise the event to those subscribers 
- the ex of the subscriber will be the cart menu item in the blazor component will be invoked with the quantity of the items stored in the cart changed, basicallyy when the quantity of the items in the cart changes the event will be raised and any subscribed members will be called.
- then we can unsubscribed(to avoid the memory leaks) the events using the IDisposable interface. += operator to subscribe the event and -= to unsubscribe the events.
- so before the blazor component is garbage collected, ie befor the dotnet freeze the memory taken up by the blazor component the relevant method in the blazor component must be unsubscribed from the evnt.

# payment integration (paypal)
- to facilitate the user to purchase the items in the shopping cart we will be integrating the paypal(gateway) payment service in our blazor comp.
- from the cheeckout page the user can purchase the items through the paypal gateway.
- we can grab the basic payment integration code from the paypal docs
- that code contains initPaypalButton(), and the call back methods such as createOrder(), onApproved() will be called once the payment has been approved by th paypal.

# search functionality

- the user can search the products in the product catalog based on the product category.
- we need to add a new column to the product category table in the db. where we store our relavent css code that denotes the icons to represent the product category.
- we will be using EF core code fest migration to make the changes in  our db.
- update the product category entity class to include the new str property.

# User Local storage

- by implementing this we can reduce the no.of reqs to the server.
- for the product and the shopping cart data.
- for that we can use the nouget package that provides the local storage functionality. "blazored.local storage" -> this package has the interface ILocalStorageService that we can implement in our storage service class.
- the local storage will allow us to store the kvp in the browser.
- it has no expiration, and the data will be available even after the browser is closed and are available for the future session.
- we can create 2 services to encapsulate the local storage functionality in the blazor comp.
- 1. ManagedProductLocalStorageService 2. 