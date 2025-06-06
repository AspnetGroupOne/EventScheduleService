# EventScheduleService

# Postman:

## Authentication:

All requests to this API require an API-Key to be passed in the header under "X-API-KEY". 

Invalid requests will be met with:

```json
{
    "success": false,
    "error": "Invalid api-key or api-key is missing."
}
```

## POST and PUT: 

Is made by sending a request with no id at the end. 

www.EXAMPLEURL.net/api/Schedules/

The api needs data that looks like this: (All fields it takes in are handled like strings.)

```json
{
    "eventId": "56f58514-7581-4b18-97f5-b6eb5ba7b9c5",
    "gateOpenStart": "1030",
    "gateOpenEnd": "1200",
    "preShowStart": "1300",
    "preShowEnd": "1400",
    "ceremonyStart": "1500",
    "ceremonyEnd": "1530",
    "concertStart": "1600"
}
```

If sending a POST, then the data will be added. If PUT then the data will be updated if there is any with that eventId.

## GET:

Is made by sending a request with an id at the end. 

www.EXAMPLEURL.net/api/Schedules/56f58514-7581-4b18-97f5-b6eb5ba7b9c5

And you will recieve data in json format that looks like this on success:

```json
{
    "content": {
        "eventId": "56f58514-7581-4b18-97f5-b6eb5ba7b9c5",
        "gateOpenStart": "1130",
        "gateOpenEnd": "1200",
        "preShowStart": "1300",
        "preShowEnd": "1400",
        "ceremonyStart": "1500",
        "ceremonyEnd": "1530",
        "concertStart": "1600"
    },
    "success": true,
    "statusCode": 200,
    "message": null
}
```

## DELETE

Is made by sending a request with an id at the end. 

www.EXAMPLEURL.net/api/Schedules/56f58514-7581-4b18-97f5-b6eb5ba7b9c5

Upon success you will recieve:

```json
{
    "success": true,
    "statusCode": 200,
    "message": null
}
```

# Sequence diagram plantuml

<img src="https://github.com/user-attachments/assets/0c7c4f3b-a1ae-424a-9476-1813af972536" width="400">

# Usage in the frontend:

### Adding the schedule:

Event title is going to be the name of the event in the future when everything is properly hocked up.

<img src="https://github.com/user-attachments/assets/ff2cef35-9cad-42fd-b3c9-7219730921e2" height="400">

### Showing of the schedule:

The AM/PM changes depending on if the first two digits is higher or lower than 12.

<img src="https://github.com/user-attachments/assets/028d3aff-bd27-4669-8a84-dd94fb8b15f9" height="200">

### Created By:

https://github.com/SimonR-prog

