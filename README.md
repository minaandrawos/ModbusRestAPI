# ModbusRestAPI

A service that exposes the Modbus protocol via a RESTful API. The service was built using C# and Mono so it should work in all platforms.

The API is as follows:
  - /Read/{deviceType}:
    - Task: Reads a number of Modbus registers. The {devicetype} could be either "coils", "holdingregisters", "inputregisters", or "inputs"
    - Body: The body will follow the format below
    ```javascript
            {
              "destination": "ip:portnumber or serial address",
              "connectiontype": 0 for serial RTU, 1 for serial ASCII, or 2 for TCP,
              "slaveid": slave address as a number,
              "address": offset number where we want to start the reads,
              "count": number of registers to read
            }
    ```
      For example, let's say we want to connect to Modbus TCP, the device TCP address is "10.1.1.1:502", 
      the connection is TCP obviously, the slave id is 1, and we want to read starting from register address 1414 for five registers. 
      Here is how the request will look like:
    ```json
            {
              "destination": "10.1.1.1:502",
              "connectiontype": 2,
              "slaveid": 1,
              "address": 1414,
              "count": 5
            }
    ```  
    
  - /Write/{devicetype}:
    - Task: Perform a write to a Modbus register. The {devicetype} could be either coils or holdingregisters
    - Body: The body will follow the format below:
    ```javascript
            {
              "destination": "ip:portnumber or serial address",
              "connectiontype": 0 for serial RTU, 1 for serial ASCII, or 2 for TCP,
              "slaveid": slave address as a number,
              "address": register offset number where we want to start the reads,
              "data": the data to write to the device. This will either be an array of bools or ushorts
            }
    ```
    For example, let's say we want to connect to Modbus TCP, the device TCP address is "10.1.1.1:502", 
    the connection is TCP as well, the slave id is 1, we want to write to holding registers starting from register address 1414 values 
    [14,21,12]. The request will go to /Write/holdingregisters:
    ```json
            {
              "destination": "10.1.1.1:502",
              "connectiontype": 2,
              "slaveid": 1,
              "address": 1414,
              "data": [14,21,12]
            }
    ```
    
