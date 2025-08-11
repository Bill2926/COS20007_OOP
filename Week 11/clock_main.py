import tracemalloc

class Counter:
    def __init__(self, name, count): 
        # Constructor like C#
        self.name = name
        self.count = count
    
    def tick(self):
        self.count += 1
        return self.count
    
    def reset(self):
        self.count = 0
        return self.count

class Clock:
    def __init__(self):
        # doesn't need to pass in the class name
        self.second = Counter("second", 0)
        self.minute = Counter("minute", 0)
        self.hour = Counter("hour", 0)

    def time(self, i):
        for _ in range(i):  
            if self.second.tick() == 60:
                self.second.reset()
                if self.minute.tick() == 60:
                    self.minute.reset()
                    if self.hour.tick() == 24:
                        print("THE END OF THE DAY!")

    def reset(self):
        self.second.reset()
        self.minute.reset()
        self.hour.reset()
    
    def show(self):
        print(f"{self.hour.count:02}:{self.minute.count:02}:{self.second.count:02}")


tracemalloc.start()

clock = Clock()

clock.time(60) #1 min
clock.show()
clock.reset()

clock.time(3600) #1 hour
clock.show()
clock.reset()

clock.time(43200) #12 hours
clock.show()
clock.reset()

clock.time(86400) #full day
clock.show()
clock.reset()

current, peak = tracemalloc.get_traced_memory()
print(f"Current memory: {current / 1024:.2f} KB")
print(f"Peak memory: {peak / 1024:.2f} KB")
tracemalloc.stop()
