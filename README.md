# BugTrackerCPAPI
.net6 Function v4 app.

## Endpoints
- Get http://localhost:7178/api/bug
- Post http://localhost:7178/api/bug
with body:
{
    "id": "db8e1e46-8bc7-4d2d-99b7-1733898325f5",
    "name": "A bug",
    "description": "This should be fixed because something...",
    "created": "2022-01-17T12:00:00",
    "lastUpdated": "2022-01-17T12:00:00",
    "assignees": [
        {
            "id": "db8e1e46-8bc7-4d2d-99b7-1733898325f4",
            "name": "Test Person"
        }
    ],
    "status": "Open"
}

- Delete http://localhost:7178/api/bug/{Guid:id}

- Get http://localhost:7178/api/assignee
- Post http://localhost:7178/api/assignee
with body:
{
    "id": "db8e1e46-8bc7-4d2d-99b7-1733898325f4",
    "name": "Test Person"
}

- Delete http://localhost:7178/api/assignee

## Instructions for running
Download and install Visual Studio/Visual Studio Code and install .net6 and azure functions sdks.
Clone repo and open solution and run debug.
The API will need to be running on Port 7178 for the front end to find it.
You may need to install Microsoft Azure Storage Explorer in order to explore the Tables that are being used for persistence.

## Future work
If I gave this more time I would:
- Implement an Update function for Bug and Assignee
- Write more tests and extract mapping methods as extension functions and test separately
