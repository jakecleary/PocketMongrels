Entities
========

User
----
- string : Name

Animal
------
- string   : Name
- Type     : Type
- float    : Happiness
- float    : Hunger
- DateTime : LastPet
- DateTime : LastFeed


Type
----
- string : Name
- int    : HappinessModifier
- int    : HungerModifier

Routes
======

users
-----
POST /users
GET  /users/{id}

animals
-------
POST /users/{id}/animals
GET  /users/{id}/animals
GET  /users/{id}/animals/{id}

feeding/petting
---------------
POST /users/{id}/animals/{id}/feed
POST /users/{id}/animals/{id}/pet