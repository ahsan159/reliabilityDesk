02-Feb-2023
SUPARCO Reliability Desk Software development started. Setting up SVN repositories for version and source control. 
M Ahsan (5462)

06-Feb-2023
Added the functionality of adding parts and assemblies to main project tree node and successfully written the code for created xml type project file. However, still struggling to read back the file. 
M Ahsan (5462)

07-Feb-2023
Whole day waseted on recreated the project from the saved xml project file. XmlReader and TreeNode method in recursive mode is not producing any results.
M Ahsan (5462)

08-Feb-2023
01:12 PM
Using XElement and XmlReader with IENumerable is giving less than OK results. More working is required in project saving and opening. 
03:44 PM 
1. Finally read the project tree successfully using XElement and getting childern by calling XElement.Elements(). This will return the IENumerable of all the child elements that can be added recursively to get the elements. 
2. Now for failed attempts
a. Do not use XmlReader and TreeNode recursively of iteratively sometimes the tree will be read successfully but it will not be read properly and order will be lost and project tree will be incomplete even with 3 to 4 level of nodes
b. Calling XmlReader.ReadSubTree will also run in to problems as going back to parent level will not be successful as there is not method of telling that we have reached the last sibling node in the tree. Subsequently large files will not be read properly. 
3. The only method of calling the method recursively is knowing before hand that iterator has reached the last element and we have to go back to parent level node. XElement.HasElements and XElements.Elements functions exactly provide this. 
4. Next step is to add the attributes and reading and writting them properly. these attributes will be used for calculations and the properties of the tree nodes. 

M Ahsan (5462)

13-Feb-2023
11:37AM 
Successfully developed read and write XML to Project Tree utilities now adding attributes to it. Started making project.cs class for processing of projects and its data as object.

M Ahsan (5462)

14-Feb-2023
12:00AM
Developing a way to present series and parallel connectivity in xml file. This has to be done by instance initiation as existing data will be sorted in series and parallel configurations of reliability block diagram. This has to be extendable from project to assembly, assembly to parts and self connectivity of parts. Currently, adding the attributes to treenode as a treenode object.

20-Feb-2023
04:00PM
Created a partlist file for testing parts data. Currently, saving and retrieving of assembly and part data is started and previous work of attribute and class making is put aside for time being to develop the full project tree implementation.

23-Feb-2023
03:24PM
Sucessfully connected TreeNode and XML utilities with reliability desk project. Tomorrow, Project and Assembly classes will be developed to carter for the node properties. Tomorrow, target will be completion of project/assembly/part class and displaying the properties from the context menu as well.

24-Feb-2023
03:41PM
Added part class and created part selection form for adding existing part from partlist. Succesfully connected partlist xml to parts class.

27-Feb-2023
04:10PM
Updated selectPart and added functionalities of open,selection still working on filtered selection. still adding new part and save parts list problems

28-Feb-2023
03:31PM
Finally created a fully functional parts management kit. Now, will move to main reliability desk creation and forward this parts project to ubaid for testing.

03-Mar-2023
03:38PM
Need to supply username and login level to all desks from  login form. To avoid passing variables interprocess communication (IPC) method is selected and is being implemented using namedPipeServerStream and namedPipeClientStream. I have managed how to work and communicate. Now, Monday will see the proper implmentation and detaching the process as seperate thread. inshallah

06-Mar-2023
04:14PM
Some editing made the multiple users connect to server in IPC and login are now working properly. Alhamdullilah