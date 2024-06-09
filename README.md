# PT_Demo_Kafka

## Contents

- [AWS SQS Queue / Apache Kafka / Rabbit MQ](#aws-sqs-queue-vs-apache-kafka-vs-rabbit-mq)
- [Demos](#demos)
    - [Prerequisites](#prerequisites)
    - [Demo Kafka 2 Consumers 1 Producer](#demo-kafka-2-consumers-1-producer)
    - [Demo Kafka UI](#demo-kafka-ui)
- [Links](#links)

## AWS SQS Queue vs. Apache Kafka vs. Rabbit MQ

## Demos

### Prerequisites

1. Run `Kafka` and its dependency `Zookeeper` locally in Docker containers using the following `docker-compose.yml` file:

```
version: '3.8'
services:
  # Apache ZooKeeper is an open-source server for highly reliable distributed coordination of cloud applications. Zookeeper is a project of the Apache Software Foundation.
  zookeeper:
    image: confluentinc/cp-zookeeper:7.3.0
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000

  kafka:
    image: confluentinc/cp-kafka:7.3.0
    depends_on:
      - zookeeper
    ports:
      - "9092:9092"
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://localhost:9092
      KAFKA_LISTENERS: PLAINTEXT://0.0.0.0:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
```

2. Next, run the containers:

```
docker-compose up -d
```

💡 SUGGESTION: In case you need to use UI for Kafka, see section [Demo Kafka UI](#demo-kafka-ui).

### Demo Kafka 2 Consumers 1 Producer

1. Create a blank Solution `Demo.Kafka` including the following .NET 8 Console Applications:
- Demo.Kafka.ConsoleApp.Producer
- Demo.Kafka.ConsoleApp.Consumer1
- Demo.Kafka.ConsoleApp.Consumer2

2. Install `Confluent.Kafka (2.4.0)` NuGet package in all 3 console applications.

3. In `Demo.Kafka.ConsoleApp.Producer`, add the code that you will find in the repo.

4. In `Demo.Kafka.ConsoleApp.Consumer1` and `Demo.Kafka.ConsoleApp.Consumer2`, add the code that you will find in the repo.

💡 SUGGESTION: Pay attention to the `ConsumerConfig`:

```
        var config = new ConsumerConfig
        {
            GroupId = "group-consumer-abc",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
```

In case both `Consumer` applications have the same `GroupId`, the messages published by the `Producer` will only be consumed by any of the `Consumers` in group `group-consumer-abc`:

![kafka-1](./res/kafka-messages-read-1.png)

In case the 2 Consumers have`ConsumerConfig` with different `GroupId`-s (meaning they belong to different groups) all the messages will be read by all the consumers:

![kafka-2](./res/kafka-messages-read-2.png)

5. Run the `Producer` and the 2 `Consumer`-s and check the results when `Producer` starts publishing messages...

### Demo Kafka UI

## Links