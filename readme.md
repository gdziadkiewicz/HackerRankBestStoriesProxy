### HackerRank Best Stories Proxy
This application serves as a proxy for the Hacker News (HN) best stories API, providing a RESTful interface using ASP.NET Core. It addresses the UX issue of needing multiple calls to get story details by caching the data and refreshing it periodically.

### How to Run

1. **Clone the repository:**
    ```sh
    git clone https://github.com/yourusername/HackerRankBestStoriesProxy.git
    cd HackerRankBestStoriesProxy
    ```

2. **Run the application:**
    ```sh
    dotnet run
    ```

3. **Access the API:**
    The API will be available at `http://localhost:5027` by default. Test it by opening/calling http://localhost:5027/beststories/2 .


### How does it work
* Happy path: data is refreshed with a 10s delay between refreshes. The preassure on HN api is limited and the users of the app expirience consistent response time and data up to 10s+refresh time(~3s) old.
* Unhappy path: the background service is retrying with a 10s delay between attempts. Errors are getting logged. Latest available data from the cache is served to the users for 1m, after that they start recieving 404 errors.

### Assumptions made
* We do want to have consistent response times for all users and wanting to be ready for the users justifies polling the HN API in a loop.
* We are OK with serving data which can be stale to some degree(~10s) in order to optimize requests to HN API.
* We want to mask minor transient errors with stale data(up to 1m)
* We want the service to go down on HN API breaking changes in JSON schema.

### Things to consider
* configure retries and backoff for the HTTP clinet, especially if there is will to increase the refresh rate
* add alerts from the background service to get notified when it brakes
* better error reporting to the user (temporal problems vs full blown fail)