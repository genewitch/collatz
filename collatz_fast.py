import time
temptime = time.perf_counter()
time.sleep(1)
print(time.perf_counter()-temptime, end='')
print('should be about equal to 1.000000000000')

working = 0
lastvalue = 0
runningtally = 0
count = 0
highestcount = 0
currentvalue = 1

if currentvalue % 2 == 0:
    currentvalue += 1

starttime = time.perf_counter()

while (True):
    if ((runningtally % 500001) <= 0):
        snaptime = time.perf_counter()
        elapsedmilliseconds = round((snaptime-starttime)*1000)
        print(elapsedmilliseconds, end='')
        #ps = runningtally / snaptime
        ps = round(runningtally / (elapsedmilliseconds+1))
        print("ms    steps/ms: " + str(ps) + "    total steps: " + str(runningtally) + ", last number's stepcount:" + str(count))

    elif count > highestcount:
        highestcount = count
        timestamp = time.ctime(time.time())
        print()
        print(str(timestamp) + " New record: " + str(highestcount) + ", on: " + str(currentvalue) + ".")

    lastvalue = currentvalue
    currentvalue += 2
    count = 1
    working = currentvalue
    while working > 1:

        if working <= lastvalue:
            break

        elif working % 2 == 0:
            working = working / 2
        
        else:
            working = ((working*3) +1) / 2

        count += 1
        runningtally += 1
