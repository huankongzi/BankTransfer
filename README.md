There are two projects, one is frontend with React, the other is ASP.NET Core Web API.

1. Frontend

In the front folder, run "npm start" to open web application.

2. Backend

Open Backend.sln in Visual Studio with Administrator permission if on Windows system. Please use https://localhost:5001 to host application due that the URL is hardcode in frontend. It should be optimized if extra time to avoid hardcode.

3. Problems need to solve.
React and Ant Design are totally new to me. Due that Sunday is workday, and I have personal affairs in Saturday. I have only two nights to work on this.
There is an issue which is not solved, uploading file cross domain issue. But other APIs have no this issue. Any API about reading file in action method will throw this issue. It is very hard to visit global website. I have less resource to search out the problem. 

4. Workaround
I put a file in server side, reading the content if user click "Get Data¡" button.

5. Function test
Click "Get Data" button shows in the Tests/1.png.
Click one search button at the end of a line shows in the Test/2.png.
