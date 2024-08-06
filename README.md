# Stories - API Rest

This project was generated with [Visual Studio 2022] with a template offered

## Features

	- APIRest 
    - DDD pattern to Domain development
    - Repository gets data from external service instead of ORM,  manteining the concept.	
    - Exceptions and Validation integrated in a middelware
	- Async Pattern
	- Swager integration
	

## Running API to verify functionalities, by default, using Swagger UI from the IDE 
   
    1 - Get the newest stories
       - URL:'http://localhost:5165/stories-api/Story/GetStories?OrderBy=[orderbyparameter]&Limit=[limitparameter]'
       - Assumptions: The endpoint returns the newest stories.  
	                  The parameters are required.
                      The [orderbyparameter] parameter only is valid for a specific value: 'priority' .					  
					  The second one parameter 'limit' is available to values between 1 and 50.
					  Examples
					  Successful Case:  
					  
										[					  
										  {
											"id": 41169981,
											"title": "Rodney Brooks' Three Laws of Robotics",
											"url": "https://spectrum.ieee.org/rodney-brooks-three-laws-robotics"
										  },
										  {
											"id": 41169971,
											"title": "AI is reacting to Twitter live",
											"url": ""
										  },
										  ...
										  ...
										  {
											"id": 41169938,
											"title": "'Framework': An Advanced Laravel Development Suite",
											"url": "https://github.com/laravel/framework"
										  }
										 ]
					  
					  Not validated case: http://localhost:5165/stories-api/Story/GetStories?OrderBy=anyvalue&Limit=50
					  
						Error: Not Found

						Response body
						Download
						{
						  "ContextId": "0HN5M1IA41KOT:00000002",
						  "Messages": [
							{
							  "CategoryId": 0,
							  "Category": "General",
							  "Name": "StoriesNotFound",
							  "Text": "The Order by parameter is not able",
							  "Value": null
							}
						  ]
					  
					  - Caching handler: All stories are stored into memory cache for five minutes. 
									   The Cache only is updated earlier to 5 minutes when the parameters has been changed. 
									   
					  
					  
					  
	2 - Get specific Story 					  
							URL: http://localhost:5165/stories-api/Story/GetStory?id=[idstory]
							The endpoint returns a story.  
							The parameter is required.

							Examples
							Successful Case:  URL: http://localhost:5165/stories-api/Story/GetStory?id=41169981
							200	
							Response body
							Download
							{
							  "id": 41169981,
							  "title": "Rodney Brooks' Three Laws of Robotics",
							  "url": "https://spectrum.ieee.org/rodney-brooks-three-laws-robotics"
							}
	  
							Not validated case: URL: http://localhost:5165/stories-api/Story/GetStory?id=9999999999999999
							
							
							Code	Details
							404
							Undocumented
							Error: Not Found

							Response body
							Download
							{
							  "ContextId": "0HN5M1IA41KOU:00000002",
							  "Messages": [
								{
								  "CategoryId": 0,
								  "Category": "General",
								  "Name": "StoryNotFound",
								  "Text": "The story have not been recovered.",
								  "Value": null
								}
							  ]
							}



## Effort assigned to the API development : 3 days
