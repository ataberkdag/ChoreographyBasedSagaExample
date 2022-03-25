# Choreography-based Saga Example Project

Dockerized Choreography-based Saga Example Project with Onion Architecture. Includes Order, Stock and Payment microservices.

### Order.API

#### General

- Publisher and Consumer. (Publish to StockAPI. Consumes from StockAPI and PaymentAPI.)

- [x] RabbitMQ with MassTransit

- [x] EntityFramework Core - PostgreSQL

- [x] Mediator Pattern

- [x] Repository Pattern

- [x] Unit of Work Pattern

- [x] Dockerized

#### Events

##### Publish

- OrderCreatedEvent

##### Consume

- StockNotReservedEvent

- PaymentFailedEvent


### Stock.API

#### General

- Publisher and Consumer. (Publish to PaymentAPI. Consumes from OrderAPI and PaymentAPI.)

- [x] RabbitMQ with MassTransit

- [x] EntityFramework Core - PostgreSQL

- [x] Mediator Pattern

- [x] Repository Pattern

- [x] Unit of Work Pattern

- [x] Dockerized

#### Events

##### Publish

- StockReservedEvent

- StockNotReservedEvent

##### Consume

- OrderCreatedEvent

- PaymentFailedEvent


### Payment.API

Receives successful stock message. 

#### General

- [x] Publisher and Consumer. (Publish event. Consumes from StockAPI.)

- [x] RabbitMQ with MassTransit

- [x] Mediator Pattern

- [x] Dockerized

#### Events

##### Publish

- PaymentSucceededEvent

- PaymentFailedEvent

##### Consume

- StockReservedEvent

## Run with Docker


```bash
docker-compose -f docker-compose.yml up -d
```

## Migration

To apply migrations follow this command on Package Manager Console for Order and Stock Microservices. (Set starting project to API and set default project to Infrastructure on Package Manager Console)

```bash
update-database
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
