P2K05Seem v1.0 Release Candidate 1	- August 3, 2007

STANDARD WARRANTY: This software comes with absolutely no warranty and I am not responsible for any damage it may cause.

TESTERS:
	+ This is a private beta do not distribute, the final version will be released publicly
	  soon.
	+ I need bug reports for any and all bugs you find or anything you think should be changed.
	+ I need to know what OS's this works on, currently I have only tested Windows 2000/XP (x86)
	+ I need to know what phones you tested and what worked and what didn't, this is crucial
	  because I have a very limited number of models I can test myself.
	+ I need to know what firmwares work with it and if any don't
	+ The included functions.csv is relatively out of date, updated versions are welcome!
	  - CSV files for particular phone models like v9m, k1m, etc... are welcome!

	======================================================
	+ PLEASE SEND ALL BUG REPORTS TO p2k05seem@gmail.com +
	======================================================
	
Requirements:
	- Recent Motorola Handset Drivers
	  * Can be obtained at: http://developer.motorola.com/docstools/USB_Drivers/
	- The Microsoft .NET framework 2.0 or above
	  * Can be obtained at: (x86) http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en
							(x64) http://www.microsoft.com/downloads/details.aspx?familyid=B44A0000-ACF8-4FA1-AFFB-40E78D788B00&displaylang=en
	- Windows 2000/XP/Vista 

Features:
	-<WILL BE WRITTEN BEFORE 1.0.0 FINAL is released>

Things to know:
	- Function.csv Info:
	  * The format for Functions.csv is firm and not at all flexible.  
		Each line of the file must be in the following format:
		"<SEEM>";"<RECORD>";"<OFFSET>";"<BIT>";"<BIT DESCRIPTION>";"<CHECKED STATE>";"<UNCHECKED STATE>"
	  * For lines that are a general description of the entire seem set the OFFSET to -1
		the <BIT DESCRIPTION> for these lines will be displayed in the "Info:" label in the
		Selected Byte Details group box. 
	  * Be aware that the that when the <OFFSET> is -1 the <BIT>, <CHECKED STATE>, and
		<UNCHECKED STATE> fields are ignored, but CANNOT BE EMPTY.
	  * The <SEEM>, <RECORD>, and <OFFSET> fields must be in HEX
	  * Any quotes (") in the <BIT DESCRIPTION> field will be removed.
	  * The <BIT> field must only be the digits 0-7 if it consists of anything else the line
		is ignored.
	  * Any blank lines or lines which fail to conform to the field descriptions above will
		be ignored.
	  * Additionaly any lines missing any field will be ignored.

	- Phone Support
	  * Known to work:
		+ v3m
		+ k1m
		+ v9m
		+ w385
	  * Known NOT to work:
		+ e815/e816
		+ v3c
	    
	  * Currently only v3m and newer are known to be supported but I will try hard to get
		older generation phones working as well.

	- Strange Seems
	 * Some seems always pull at their maximum length even if you set bytes to just 1, this
	   isn't a bug in the application but in fact what the phone returns no matter how many
	   bytes you ask it to return, 01d1 is an example of this.  Since some of these seems
	   cannot be partially written to and must be written at full length I decided not to
	   chop the extra bytes off as this would limit the usefulness of this tool.
	 * Many seems can be pulled but not at length 0.  You may notice that most seems can
	   pulled at length 0 and when you do this it returns the entire seems.  There are
	   certain seems however (like 0000 or 0101) which are longer than the p2k/p2k05
	   maximum byte limit (4077 bytes for p2k and 4083 for p2k05) and thus you must specify
	   a length to pull them at.  This is a limitation of the protocols and not this tool.
 
 Copyrights:
 p2kapi 3.0 Beta ©Copyright Vilko, dwALX, s5vi, motoprogger
 Be.Windows.Form.Hexbox ©Copyright Bernhard Elbl
 p2k05seem ©Copyright 2008 null1281
 
 