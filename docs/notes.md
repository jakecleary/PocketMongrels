Notes
=====

Questions
---------
- How to properly unpack request bodies.
- How to properly format responses (HttpResponseMessage etc)

Next steps
----------
- Add a PetService that is responsible for any logic 
  surrounding pets. Currently the controllers are doing to much (restoring 
  hunger/happiness, resetting timers etc).
- Split the petting/feeding out as 'action' resources. This would flatten
  the URI structure out a bit which currently uses chaining too much in 
  my opinion.

  ### Example:

  #### Request
  
  ```json
  POST /api/actions/

  {
      "Type": "feed",
      "User": "cca2854f-4b33-43cb-a34a-0102221fe2f1",
      "Pet": "baf61961-2f3b-4e46-9faf-af5ad3d13860"
  }
  ```

  #### Response

  ```json
  HTTP OK
  Location: http://blah.com/api/animals/baf61961-2f3b-4e46-9faf-af5ad3d13860

  {
    "Guid": "baf61961-2f3b-4e46-9faf-af5ad3d13860",
    "Name": "Aurthur the Aardvark",
    "Type": 2,
    "Hunger": 0,
    "Happiness": 9.95,
    "LastFeed": "2016-12-07T12:40:18.7789036Z",
    "LastPet": "2016-12-07T12:41:01.0412902Z",
    "Born": "2016-12-07T12:37:42.8485645Z",
    "Owner" {
        "Guid": "e497ea45-76ee-49c4-b1be-70732176aa6e",
        "Name": "Jake"
    }
  }
  ```

- Add a DB and use some kind of sceduled task to calculate the metrics, 
  rather than the convoluted getter.