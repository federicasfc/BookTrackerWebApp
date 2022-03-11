# BookTrackerWebApp

-Purpose of App:
  - This project was created for forgettful readers to keep track of the next books they should be reading, and what they have most recently read.
  - Instead of letting forgotten books pile up around the house without being read, readers will be able to separate their future reads by those they haven't gotten yet, the ones they have, and the ones they have started reading.
  - In addition to adding books to their user-specific lists, users are also able to add books and genres to the database
  - all logged-in users have access to the books and genres of the database, including those they have not personally added. This allows for users to discover new books to add to their lists.
  
-Tables and Functionality:
  - Books/Genres:
    - GetAll: users can see all books/genres
    - GetById: users can look at individual book/genre details
    - Create/Add: users may add new books/genres to the database that are accessible to all other users
    - Update: users may update existing books/genres in the database
    - Delete: users may remove books/genres from the database
    
    - FK relationship and additional functionality:
      - Books and Genres have a many to many relationship:
        - Users are able to add genres to books and update genres associated with books via the Book Create and Edit pages
        - Users are able to see the list of all books associated with a particular genre via the Genre BookList genre page

  - ReadingList:
    - ReadingList is a table composed of a base class (ReadingList) that has two derived classes that inherit from it (AcquiredList and ReadList)
    - Inheritance hierarchy: ReadingList -> AcquiredList -> ReadList
    - At each level, more properties are added, providing more details about a user's relationship with a certain book.
    - Each level has its own CRUD functionality with individual services and controllers
    
    - ReadingList:
      - Purpose: Houses books that users wish to read, but have not acquired
    - AcquiredList:
      - Purpose: Houses books that users have acquired, but have not read yet
    - ReadList:
      - Purpose: Houses books that users have both acquired and begun to read; Includes a bool property that allows users to determine whether or not they have finished reading it
    
    - FK Relationships and Additional Functionality:
      - ReadingList takes in both a BookId and a UserId. It acts as the connection between Books and individual users; one user may add many books to their lists.
      - Users are also able to move books that already exist in a list to another list:
        - If a book is in the ReadingList, it may be added either to the AcquiredList or to the ReadList
        - If a book is in the AcquiredList, it may be added to the ReadList
        - In both cases, the user must simply add the additional properties of the derived classes to the existing data
          - On the user side, this is done through the AcquiredList and ReadList AddFromReading pages and the ReadList AddFromAcquired page; the relevant pages are linked in the ReadingList and AcquiredList Index pages.
          
 Current Bug:
 - Because of the inheritance in ReadingList, any books added to a derived class also show up in the parent class lists displayed to the user. Will be fixed soong hopefully.
      
