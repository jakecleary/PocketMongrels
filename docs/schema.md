# Schema

## Entities

### User
- `Guid` : Id
- `string` : Name

### Animal
- `Guid` : Id
- `string` : Name
- `AnimalType` : Type
- `float` : Happiness
- `float` : Hunger
- `DateTime` : LastPet
- `DateTime` : LastFeed
- `DateTime` : Born

## Routes

### users
- `POST /users`
- `GET  /users/{id}`

### animals
- `POST /users/{id}/animals`
- `GET  /users/{id}/animals`
- `GET  /users/{id}/animals/{id}`

### feeding/petting
- `POST /users/{id}/animals/{id}/feed`
- `POST /users/{id}/animals/{id}/pet`