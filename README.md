# Choreography-based Saga Example Project

Dockerized Choreography-based Saga Example Project with Order, Stock and Payment microservices.

### Order.API

- [x] MassTransit and RabbitMQ

- [x] EntityFramework Core - PostgreSQL

- [x] Mediator Pattern

- [x] Repository Pattern

- [x] Unit of Work Pattern

- [ ] Fluent Validator

- [ ] Behaviours

### Stock.API

...

## Run with Docker

Use the package manager [pip](https://pip.pypa.io/en/stable/) to install foobar.

```bash
docker-compose -f docker-compose.yml up -d
```

## Migration

To apply migrations follow this command on Package Manager Console. (Set starting project to API and set default project to Infrastructure on Package Manager Console)

```bash
update-database
```
## License
[MIT](https://choosealicense.com/licenses/mit/)