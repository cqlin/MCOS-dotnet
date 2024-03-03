<%
	SMTPSERVER = "mcos.cbcm.org"
	SMTPPORT   = 588 
	EMAILFROM  = "mailmaster.cbcm@cbcm.org"
	EMAILTO    = "command@cbcm.org" 

    Dim cdoConfig
    Dim cdoMessage

    VDONE        = "N"
	VADMIN       = request("admin")
	VSUBMIT      = request("Submit")
	VID          = request("ID")
	VEMAIL       = request("EMAIL")
	VCMD         = request("cmd")
	if(VCMD = "")then VCMD = "RELEASE"

	if(((VSUBMIT = "Submit") or (VADMIN = "Y")) and (VID <> ""))then
		if(VEMAIL = "") then VEMAIL   = EMAILFROM
		VDONE        = "Y"

		if(VCMD = "RELEASE")then
			VTO          = EMAILTO 
			VSUBJECT     = "[C] RELEASE " & VID & " " & VEMAIL
			VBODY        = "admin: " & VADMIN & vbcrlf
			
			sch = "http://schemas.microsoft.com/cdo/configuration/" 
			Set cdoConfig = CreateObject("CDO.Configuration") 
			With cdoConfig.Fields 
				.Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
				.Item(sch & "smtpserver") = SMTPSERVER
		        .Item(sch & "smtpserverport") = SMTPPORT
				.update 
			End With 
	 
			Set cdoMessage = CreateObject("CDO.Message") 
			With cdoMessage 
				Set .Configuration = cdoConfig 
				.From = EMAILFROM 
				.To = VTO 
				.Subject = VSUBJECT 
				.TextBody = VBODY
				.Send 
			End With 
	 
			Set cdoMessage = Nothing 
			Set cdoConfig = Nothing 

			VMESSAGE = "Thank you! We'll release the message " & VID & " soon."
		end if
		if(VCMD = "DELETE")then
			VTO          = EMAILTO
			VSUBJECT     = "[C] DELETE " & VID
			VBODY        = "admin: " & VADMIN & vbcrlf

			sch = "http://schemas.microsoft.com/cdo/configuration/" 
			Set cdoConfig = CreateObject("CDO.Configuration") 
			With cdoConfig.Fields 
				.Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
				.Item(sch & "smtpserver") = SMTPSERVER
		        .Item(sch & "smtpserverport") = SMTPPORT
				.update 
			End With 
	 
			Set cdoMessage = CreateObject("CDO.Message") 
			With cdoMessage 
				Set .Configuration = cdoConfig 
				.From = EMAILFROM 
				.To = VTO 
				.Subject = VSUBJECT 
				.TextBody = VBODY
				.Send 
			End With 
	 
			Set cdoMessage = Nothing 
			Set cdoConfig = Nothing 
			
			VMESSAGE = "Thank you! We'll delete the message " & VID & " soon."
		end if
	end if		
%>
<html>
<head>
<title>Welcome to the Chinese Bible Church of Maryland</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<LINK rel=stylesheet type="text/css" href="/ScreenSchema.css" title="Overall Screen Scheme">
<LINK REL="SHORTCUT ICON" HREF="https://webmail.cbcm.org/home_page_pictures/favicon.ico">

<script>
<!--

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && document.getElementById) x=document.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
</script>

<style type="text/css">
<!--
.style2 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 10px;
	font-weight: bold;
}
.style4 {font-family: Arial, Helvetica, sans-serif; font-size: 12px; }
.style6 {font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; }
.style7 {
	color: #0000FF;
	font-weight: bold;
	font-style: italic;
}
.style9 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #FF0000;
	font-weight: bold;
}
.style11 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 14px;
	font-weight: bold;
}
-->
</style>
</head>

<body bgcolor="#FFFFFF" text="#000000" onLoad="MM_preloadImages('/home_page_pictures/cbcm_banner_over.gif')">
<table border="0" cellspacing="0" cellpadding="0" width="740">
  <tr> 
    <td height="102" valign="top"><a href="http://www.cbcm.org/" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('cbcm_banner','','/home_page_pictures/cbcm_banner_over.gif',1)"><img name="cbcm_banner" border="0" src="/home_page_pictures/cbcm_banner.gif" width="740" height="100"></a></td>
  </tr>
</table>

<table width="736" border="0" cellspacing="0" cellpadding="5" height="100%">
  <tr> 
    <td valign="top" width="6%" height="843"> 
      <table width="89" border="0" cellpadding="5">
        <tr> 
          <td height="89" valign="top" bgcolor="#4B50D0"> <img src="/home_page_pictures/cbcm_logo_small.jpg" width="100" height="100"></td>
        </tr>
      </table>
    </td>
    <td valign="top" width="94%" height="843"> 
      <table border="0" cellspacing="0" cellpadding="5" width="606">
        <tr valign="top"> 
          <td height="41" width="383"> 
            <p><font face="Arial, Helvetica, sans-serif" size="4"><b><i><font color="#000000">Release Quarantined Email              <img src="/home_page_pictures/line.gif" width="392" height="6"></font></i></b></font></p>
          </td>
          <td height="41" width="223">
            <div align="right"><font face="Arial, Helvetica, sans-serif" size="1"> 
              <script language="JavaScript1.2">
				var months=new Array(13);
				months[1]="January";
				months[2]="February";
				months[3]="March";
				months[4]="April";
				months[5]="May";
				months[6]="June";
				months[7]="July";
				months[8]="August";
				months[9]="September";
				months[10]="October";
				months[11]="November";
				months[12]="December";

				var time=new Date();
				var lmonth=months[time.getMonth() + 1];
				var date=time.getDate();
				var year=time.getYear();
				if (year < 2000) 
					year = year + 1900; 
				document.write(lmonth + " ");
				document.write(date + ", " + year);
				</script>
            </font></div>
          </td>
        </tr>
      </table>  

      <table border="0" width="606" cellspacing="0" cellpadding="0">

<%	if(VDONE = "Y")then	 %>
               <tr> 
                  <td height="21" valign="top" bgcolor="#FFFFFF"><font color="#FF0000" size="4"><strong>
				  <%=VMESSAGE%></strong></font><font face="Arial, Helvetica, sans-serif" size="4"></font></td>
               </tr>
<%	else %>
			<tr>
				<td width="100%">
					<form action="release.asp" method="post" name="release" id="release">
					  <table width="100%" border="1">
						<tr> 
						  <td width="47%"><font size="3" face="Arial, Helvetica, sans-serif">ID</font></td>
						  <td width="53%"><font face="Arial, Helvetica, sans-serif"> 
							<input name="ID" type="text" id="ID" size="32" maxlength="50" value="<%=VID%>">
							</font></td>
						</tr>
						<tr> 
						  <td><font size="3" face="Arial, Helvetica, sans-serif">Your Email Address (optional)</font></td>
						  <td><font face="Arial, Helvetica, sans-serif"> 
							<input name="EMAIL" type="text" id="EMAIL" size="24" maxlength="76" value="<%=VEMAIL%>">
							</font></td>
						</tr>
					  </table>
					  <input type="submit" name="Submit" value="Submit">
					  <br>
					</form>
				</td>
			</tr>
<%	end if %>		   
			<tr valign="top">
			  <td height="2" colspan="4"><font face="Arial, Helvetica, sans-serif" size="1">If you sent us an email but your email was rejected, it was because your email address was not on our &quot;white list.&quot; Please send an email to <a href="mailto:whitelist@cbcm.org?subject=Put%20Me%20into%20Church%27s%20White%20List">whitelist@cbcm.org</a>, and we will include your email address on our white list. Then you can send the email again, and it will get through this time. Thank you for your cooperation!</font></td>
			</tr>
      </table>

      <div align="center"><br>
        <font face="Arial, Helvetica, sans-serif" size="1">Chinese Bible Church of Maryland</font> </div>
    </td>
  </tr>
</table>
<p>&nbsp;</p>
<p>&nbsp;</p></body>
</html>

