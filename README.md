# Reliability Desk
## 02-Feb-2023
SUPARCO Reliability Desk Software development started. Setting up SVN repositories for version and source control. 
Ahsan Saddique  

## 06-Feb-2023
Added the functionality of adding parts and assemblies to main project tree node and successfully written the code for created xml type project file. However, still struggling to read back the file. 
Ahsan Saddique  

## 07-Feb-2023
Whole day waseted on recreated the project from the saved xml project file. XmlReader and TreeNode method in recursive mode is not producing any results.
Ahsan Saddique  

## 08-Feb-2023
01:12 PM
Using XElement and XmlReader with IENumerable is giving less than OK results. More working is required in project saving and opening. 
03:44 PM 
1. Finally read the project tree successfully using XElement and getting childern by calling XElement.Elements(). This will return the IENumerable of all the child elements that can be added recursively to get the elements. 
2. Now for failed attempts
a. Do not use XmlReader and TreeNode recursively of iteratively sometimes the tree will be read successfully but it will not be read properly and order will be lost and project tree will be incomplete even with 3 to 4 level of nodes
b. Calling XmlReader.ReadSubTree will also run in to problems as going back to parent level will not be successful as there is not method of telling that we have reached the last sibling node in the tree. Subsequently large files will not be read properly. 
3. The only method of calling the method recursively is knowing before hand that iterator has reached the last element and we have to go back to parent level node. XElement.HasElements and XElements.Elements functions exactly provide this. 
4. Next step is to add the attributes and reading and writting them properly. these attributes will be used for calculations and the properties of the tree nodes. 

Ahsan Saddique  

## 13-Feb-2023
11:37AM 
Successfully developed read and write XML to Project Tree utilities now adding attributes to it. Started making project.cs class for processing of projects and its data as object.

Ahsan Saddique  

## 14-Feb-2023
12:00AM
Developing a way to present series and parallel connectivity in xml file. This has to be done by instance initiation as existing data will be sorted in series and parallel configurations of reliability block diagram. This has to be extendable from project to assembly, assembly to parts and self connectivity of parts. Currently, adding the attributes to treenode as a treenode object.

## 20-Feb-2023
04:00PM
Created a partlist file for testing parts data. Currently, saving and retrieving of assembly and part data is started and previous work of attribute and class making is put aside for time being to develop the full project tree implementation.

## 23-Feb-2023
03:24PM
Sucessfully connected TreeNode and XML utilities with reliability desk project. Tomorrow, Project and Assembly classes will be developed to carter for the node properties. Tomorrow, target will be completion of project/assembly/part class and displaying the properties from the context menu as well.

## 24-Feb-2023
03:41PM
Added part class and created part selection form for adding existing part from partlist. Succesfully connected partlist xml to parts class.

## 27-Feb-2023
04:10PM
Updated selectPart and added functionalities of open,selection still working on filtered selection. still adding new part and save parts list problems

## 28-Feb-2023
03:31PM
Finally created a fully functional parts management kit. Now, will move to main reliability desk creation and forward this parts project to ubaid for testing.

## 03-Mar-2023
03:38PM
Need to supply username and login level to all desks from  login form. To avoid passing variables interprocess communication (IPC) method is selected and is being implemented using namedPipeServerStream and namedPipeClientStream. I have managed how to work and communicate. Now, Monday will see the proper implmentation and detaching the process as seperate thread. inshallah

## 06-Mar-2023
04:14PM
Some editing made the multiple users connect to server in IPC and login are now working properly. Alhamdullilah

## 08-Mar-2023
4:24PM
The parts properties table in the main reliability desk in now working fine. Thought of adding some fields of dataset and datatables in static class as fields to set the consistency.

## 03-Apr-2023
2:29PM
updated the few parts in parts selection and main desk. Found a way to query multiple columns from datatable. Added few global varables and enum to bind with consistency and transport of data is easier between two form and databases. Need to write assembly.cs very next thing to make the project combines and write real block diagram analysis algorithms.

## 04-Apr-2023
2:11PM
created xml facilities for assemblies and parts. need project class that can be inherited from assembly class. identification of xelement and tree node will be made by contactinating the names until parent. This required unique sublings nameing. this functionality is required to be added in main desk. Furthermore, renaming and other facilities are required to be connecting between class and treenodes. Overall connectivity of class and main desk is required to written. 

## 05-Apr-2023
2:25PM
project.cs, assembly.cs and part.cs classes almost complete. treenode has been connected using project class. some logical problem is not allowing parts to be added to treenode and restraining complete loop to iterate through all elements. First thing tomorrow is to provide testing of parts form to ubaid for testing. 

## 06-Apr-2023
3:04PM
The logical problem I was facing yesterday was the incorrect parsing of date in part class. now all the tree node is correctly populated. However, I am having difficulty in getting the assemblies populated in project class. I am thinking of using assembly class for saving the project too. as there is no difference lets do this tomorrow. Testing of part form is being done to ubaid i have forwarded the executables to him. Hope as this assembly/project issue resolovs the front end will be complete and only thing require is building the algorithm for series parallel and analysis performing scheme. INSHALLAH the first versions will be available after 1 month.

## 10-Apr-2023
2:27PM
Adding the new assemblies alogrithm is implemented. Only problem I was missing was not changing the node name and text correctly and not backtracing from parent nodes. Now, creating the renaming the assembly algorithm. I am facing some problem regarding parent path in this scenario. Hoping to solve the problem by tomorrow inshallah.

## 11-Apr-2023
3:28PM
added facility of saving and loading project from xml file. Adding parts and assemblies at any level us almost flawless. Time to forward this to ubaid for testing. Time to think about configuration and other illustrative aspects of reliability block diagram project.

## 12-Apr-2023
2:00PM
There is some problem in adding the part to project tree. It is not correctly setting part fullpath and other properties. Good News is apart some part data problem I have correctly implemented copy, paste, delete and renaming algorithms Alhamdullilah. Next step get configuration part started as reliability engine. Furthermore, added two tabs in reliability desk have to populate it with part and assembly data in tables. 

## 13-Apr-2023
2:13PM
Application is pretty much ready for front end. Overview from ubaid has generated a requirement to update/renmae the part/assembly from the front and datagridview. This will require sometime otherwise we have to move forward to writting reliability engine. Also, bug in the part level entry is now removed. Furthermore, parts and assemblies are providing full xml for reading and writting project. 

## 14-Apr-2023
11:31AM
Started working on drag and drop operations of control. Button will be used to indicate every block in block diagram analysis.

## 18-Apr-2023
2:23PM
Drag and Drop of control items is done. However, planned of drawing rectangles for unit representation and line for connetion. Successfully drawn both now draging and controlling is left for these. Hope to be done in next two days before eid holidays. 

## 22-May-2023
3:02PM
Finally learned WPF and pretty much developed the basic frontend of block diagram which will only support series path for now. But now I can add new blocks easily. Now, connecting will be left and saving in xml format will be left. 

## 23-May-2023
4:28PM
Giving better looks to app and deciding to create a series reliability connections. 

## 06-Jun-2023
12:35PM
making xslt to display part lists and parts as report and chrome page.