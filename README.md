# PersonalFinanceManagement
Personal Finance Management is a project which gives the user a set of activities he can perform on his financial data for the purpose of getting a clear picture of the incomes/expenses trends, as well as an option tp manage monthly budget and personal financial goals using different categories and subcategories.

![sss](https://user-images.githubusercontent.com/79231048/182835857-8cf2b51b-402b-4449-97c1-86e6ad0f9ca3.PNG)

For this project I used a layered architecture approach (Four tier). Which consists of the following layers:
 Presentation layer - It contains all categories related to the presentation layer.
 Business layer - It contains business logic.
 Persistence layer - It’s used for handling functions like object-relational mapping.
 Database layer - This is where all the data is stored
 
![hier](https://user-images.githubusercontent.com/79231048/182836125-2b7483db-872c-4ce0-925d-94cf2fc36b5c.PNG)

The firs functionality was importing the transactions and categories from CSV files, for which i used the CSVHelper library 

![import](https://user-images.githubusercontent.com/79231048/182836261-ea42e45f-177e-45ab-9bcb-43d0478db12e.PNG)

Other functionalities:
   List transactions with filters and pagination,
   Categorize single transaction,
   Analytical view of spending by categories and subcategories,
   Split transaction


![ends](https://user-images.githubusercontent.com/79231048/182836357-6b1ef033-e6b4-46a8-bc8d-b5249d2d77d1.PNG)

Added minimal Front-End using angular, which lists all the transactions, and enables categorization of a single transaction

![FE](https://user-images.githubusercontent.com/79231048/182836573-9f0f5391-e8ec-4d8d-a84d-91ad27547ba7.PNG)
