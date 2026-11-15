#### Authentication

### POST /api/auth/register


#### register request
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "younesasaad100@gmail.com",
  "password": "password123"
}
```

#### register response
```json
{
  "id": "sdfsdfs32-sdfsdf-sdf32sdf-33rfhhs",
  "firstName": "John",
  "lastName": "Doe",
  "email": "younesasaad100@gmail.com"
  "token": "  JhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJzZGZzZGZzMy1zZGZzZC1zZGYzM3NkZi0zM3JmaGhzIiwiaWF0IjoxNjI4MzQ1NjAwfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
```

### POST /api/auth/login

#### login request
```json
{
  "email": "younesasaad100@gmail.com",
  "password": "password123"
}
```
#### login response
```json
{
  "id": "sdfsdfs32-sdfsdf-sdf32sdf-33rfhhs",
  "firstName": "John",
  "lastName": "Doe",
  "email": "younesasaad100@gmail.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJzZGZzZGZzMy1zZGZzZC1zZGYzM3NkZi0zM3JmaGhzIiwiaWF0IjoxNjI4MzQ1NjAwfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
```
