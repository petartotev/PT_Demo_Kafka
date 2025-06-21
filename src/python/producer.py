from kafka import KafkaProducer
import random
import time
import datetime

animals = ["Cat","Dog","Giraffe","Elephant","Ant","Goat","Zebra"]

producer = KafkaProducer(bootstrap_servers='localhost:9092')

for x in range (10):
    length_animals = len(animals)
    print(length_animals)
    random_animal_index = random.randint(0, length_animals -1)
    print(random_animal_index)
    print(animals[random_animal_index])
    msg = f"{datetime.datetime.now()} | Hello from {animals[random_animal_index]}!"
    producer.send('test-topic', msg.encode('utf-8'))
    time.sleep(random_animal_index)

producer.flush()