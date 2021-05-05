### Kafka example

## Start zookeeper
docker-compose -f zk.yml up

Please note the network name here, it must be the same in the following kafka yaml.

## Start kafka 
docker-compose -f kafka.yml up


## Example .net
Run KafkaProducer first, to create topic with correct partitions and replication.

Then run KafaConsumer
