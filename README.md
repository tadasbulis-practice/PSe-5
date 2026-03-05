# PSe-5
PSe-5 workshops

Rules how we gonna work with Labs, how to deliver them to our Group repository
1. Everybody creates folder FirstameLastname
2. In our own forlder for each Lab we create folder like Lab-1, Lab-2, Lab-3.....
	For clasic GitFlow for each Lab (like a task) we create feature branch  feature/Lab3.
3. When I finish some code changes or I finish work that day, that lecture or ect. I'm making commit. Than not to loose code changes, I push  changes to remote (if branch was not pushed yet, client command is Publish.).
4. When all code code changes of the Lab is done, you commit them, push to remote.
	- feature works
	- tests passed (you have tested your feature)
	- code is cleaned.
	- checked that all classes and files in propper folders (Model classes in Models and ect. - depends on your products structure).
	- clean solution and/or project and run it again to make sure it will run and work for your team members also.
	- make sure that your feature branch and development branch doesn't has conflicts: 
		We switch to develop, Pull it to your local repository, and merge deveop into your feature branch to be sure there is no code conflicts between develop and your feature branch.
Then we make feature final commit and push changes to remote feature branch.		
5. Making Pull Request (PR):
	- make sure you have Pushed your feature branch with your code changes to origin (cloud).
	- Go with browser to your GitHub repository.
	- Select your feature branch or go to Pull Requests
	- create New Pull Request 
		- select destination branch (always develop in our case)
		- select our feature branch which we gonna make PR for.
		- optional: we can check and compare code changes which are between develop and our feature.
		- Press Create Pull.
	- IMPORTNAT: Here we need to select peaople who gonna do Code reviews - Assignees: and add your whole team, but in our case it's enought to add me (Tadas Bulis)
	- Press Create Pull Request again.
6.Code Review:
	- to be added later by all team.
