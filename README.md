Simple POC with consumer and producer in dotnet.
To run this example, the rabbitmq might running in docker. For this, after installation of docker run the command 
`docker run -d --hostname rabbit -p 5671-5672:5671-5672 -p 8080:15672 -p 25672:25672 --name rabbit rabbitmq:3.7-management`
and you can to access the rabbitmq panel in localhost:8080.

The next step you can run the project Producer and Consumer.
