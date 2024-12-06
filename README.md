# Rira Task

## برای فرخوانی سرویس ها از مدلهای زیر استفاده کنید

### CreateUser
```json
{
    "first_name": "name",
    "last_name": "name",
    "national_code": "1234567890",
    "birth_date": {
        "seconds": "1609000000",
        "nanos": 0
    }
}
```

### GetUser
```json
{
    "id":1
}
```

### UpdateUser
```json
{
    "id": 1,
    "first_name": "name",
    "last_name": "name",
    "national_code": "1234567890",
    "birth_date": {
        "seconds": "1609000000",
        "nanos": 0
    }
}
```
### DeleteUser
```json
{
    "id":1
}
```
