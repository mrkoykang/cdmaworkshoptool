feel free to submit patches or scripts and to make cdmaDevTerm better for everyone

# cdmaDevTerm - Alpha v.2.85 **12/12** #
<img src='https://sites.google.com/site/chromableedstudios/_/rsrc/1356421953406/techninjutsu/addingelysiumforwindows8wp7styleinwin7usingwpfwithc/cdmaDevTerm285smaller.png'>
<ul><li>adds ironPython scripting! script directly against the cdmaDevLib api from within cdmaDevTerm!<br>
</li><li>no ui hang during long nv item reads (watch read in real time on log tab)<br>
</li><li>returns many features from .2 that were missing in .2.7<br>
</li><li>adds 'Motorola Evdo unlock' nv item<br>
</li><li>right click esn/meid to copy converted esn/pEsn11<br>
</li><li>right click to send/write spc 000000<br>
</li><li>this is an alpha/developer preview release under GPL v3(see Readme for libraries used)<br>
</li><li>feel free to submit feedback, do bear in mind this is alpha, some users may want v.2<br>
<h1>cdmaDevTerm - Beta v.2 <b>03/12</b></h1>
<img src='http://img.photobucket.com/albums/v217/lmdgraham/cdmaDevTermV2.jpg' alt='Screenshot' border='0' width='50%' height='50%'></li></ul>

This is a beta released under the GPL v3<br>
<br>
<blockquote><a href='http://cdmaworkshoptool.googlecode.com/files/CdmaDevTermV.2.zip'> download </a></blockquote>

v2  adds:<br>
<ul><li>Data flashing is scripted by carrier.xml and model.xml<br>
</li><li>Samsung 16 digit passwords may be customized by the user in a .txt file (cdmaworkshop compatible)<br>
</li><li>Check for update tab</li></ul>


read and write:<br>
<ul><li>SPC (NV,LG,SAMSUNG,HTC,METRO PCS)<br>
</li><li>MDN / MIN<br>
</li><li>NV ITEMS^<br>
</li><li>EVDO DATA FOR CDMA DEVICES</li></ul>

write:<br>
<ul><li>PRL</li></ul>

read:<br>
<ul><li>RAM (and search for spc)</li></ul>

^cdmaDevTerm can read the full range of NvItems but I believe there are issues if you try to read the whole range at once, try the known standard range then try searching outside (or feel free to check out the source and submit a bugfix)<br>
<br>
<h1>cdmaworkshoptool / cwTool / cT.5c</h1>
<img src='http://img.photobucket.com/albums/v217/lmdgraham/ct4asc.jpg' alt='Photobucket screenshot' border='0'>
<br>
<a href='http://www.softpedia.com/get/System/File-Management/cwTool.shtml'><img src='http://www.softpedia.com/images/softpedia_download_small.gif' border='0' /></a>
<br><br>click on a spc or a meid and its copied to the clipboard!<br><br>
<br>
<br>
1. A utility that parses cdma worksop NV Item reads(to remove plain text - especially handy on new phones which may not have documented wap mms solutions) and<br>
<br>
a.) Open cwTool.exe<br>
b.) Press the button and select your NV Item Read from your hard drive<br>
c.) Open the output file C:/cwTool/out.txt in notepad and copy then paste in WinHex ( as ascii-hex ) in order to scan through an nv item read quickly and easily to find phone numbers wap setting locations etc (rather than having to copy a single nv item at a time and paste into winhex)<br>
d. Now go back to your original Nv item read (that still has all the plain text) and you can find and replace your wap settings etc<br>
e. Use cdma workshop to write back the nv items! done! yay<br>
<br>
or try your favorite hex editor<br>
<br>
2. Bin dumps(to find spc's)<br>
<br>
a.) Create your .bin dump using CDMA Workshop/UniCDMA (or whatver)<br>
b.) Open cwTool.exe<br>
b.) Select your .bin file<br>
c.) See possible SPC codes in window<br>
<br>
-submit any requests and i will consider!<br>
<br>
Change Log:<br>
<br>
.5c---<br>
- Fixed hardcoded out.txt for nv analyze<br>
<br>
.5b---<br>
- Now including: MEID/ESN conversion!!<br>
- With the very generous permission from badillo of GSM-Forum I have ported his C# code to vb .net and included it in this version<br>
<a href='http://forum.gsmhosting.com/vbb/'>http://forum.gsmhosting.com/vbb/</a>
<blockquote>(to get badillo's original app, see:<br>
<a href='http://forum.gsmhosting.com/vbb/f128/meid-pesn-converter-492647/'>http://forum.gsmhosting.com/vbb/f128/meid-pesn-converter-492647/</a>
pocket pc version even available!)</blockquote>

.4a---<br>
- attempt to fix null bug(see.3)<br>
- added gpl txt to package<br>
<br>
.3---<br>
- removed all hardcoded file locations (still don't delete that out.txt haven't tested alot)<br>
- spc is copied when you click it<br>
.3 RELEASE NOTE: although the spc search seems to work fine, a bug was introduced in .3 that generates a funny error:<br>
System.ArgumentNullException: Value cannot be null.<br>
(will be working out the kinks, as always, and hope to fix in next build... any other ideas for improvement are appreciated!)<br>
<br>
.2---<br>
- added SPC search feature<br>
- fixed hard coded input file (future release will fix hardcoded out.txt)<br>
