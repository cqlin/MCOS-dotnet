<%@ LANGUAGE=VBScript CODEPAGE=65001 %>
<%
' ----------------------------------------------------------------------------------------- '
' PROGRAM NAME : whitelist.asp                                                              '
' DATE CREATED : 02/12/2006 Edward Chen                                                     '
' DATE MODIFIED: 08/09/2007 Edward Chen                                                     '
' ----------------------------------------------------------------------------------------- '
%>
<html>
<head>
	<TITLE>CBCM - Email Extraction</TITLE>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
</head>
<body>
<%
SMTPSERVER = "mcos.cbcm.org"
SMTPPORT   = 588
EMAILFROM  = "mailmaster.cbcm@cbcm.org"
EMAILTO    = "command@cbcm.org" 


Sub SendMail(pSUBJECT, pBODY)
    sch = "http://schemas.microsoft.com/cdo/configuration/" 
    Set cdoConfig = CreateObject("CDO.Configuration") 
    With cdoConfig.Fields 
        .Item(sch & "sendusing") = 2 ' cdoSendUsingPort 
        .Item(sch & "smtpserver") = SMTPSERVER 
        .Item(sch & "smtpserverport") = SMTPPORT
        .update 
    End With 

    Set cdoMessage = CreateObject("CDO.Message") 
	VEMAIL   = EMAILFROM
	VTO      = EMAILTO
	VSUBJECT = pSUBJECT
	VBODY    = pBODY & vbcrlf

	With cdoMessage 
        Set .Configuration = cdoConfig 
        .From = VEMAIL 
	    .To = VTO 
        .Subject = VSUBJECT 
        .TextBody = VBODY
        .Send 
    End With 

    Set cdoMessage = Nothing 
    Set cdoConfig = Nothing 
End Sub


wl = ""
if request("EXTRACT") = "EXTRACT" then
	set ELIST = Server.CreateObject("Scripting.Dictionary")
	ELIST.RemoveAll
	rS = request("LIST")
	rS = replace(rS, ",", " ")
	rS = replace(rS, chr(9), " ")
	rS = replace(rS, chr(13), " ")
	rS = replace(rS, chr(10), " ")
	rS = replace(rS, ";", " ")
	rS = replace(rS, ":", " ")
	dim earr
	earr = split(rS," ")
	for i = 0 to ubound(earr)
		s = LCASE(earr(i))
		bFieldIsOkay = true
		If InStr(1, s, "@") < 2 Then
			bFieldIsOkay = False
		Else
			If InStrRev(s, ".") < InStr(1, s, "@") + 2 Then
				bFieldIsOkay = False
			End If
		End If
		if(bFieldIsOkay = true) then
			s = replace(s,"<","")
			s = replace(s,">","")
			s = replace(s,"""","")
			s = replace(s,"[","")
			s = replace(s,"]","")
			s = replace(s,")","")
			s = replace(s,"(","")
			if(Right(s,1) = ".")then
				s = Left(s, Len(s)-1)
			end if
			if((Right(s,9) <> "@cbcm.org") and (Right(s,15) <> "@cbcmgroups.org"))then
				if(not ELIST.Exists(s)) then
					if(len(s) < 40)then
						ELIST.Add s, "Y"
					end if
				end if
			end if
		end if
	next

	dS = ""
	arrKeys = ELIST.Keys
	for intLoop = 0 to ELIST.Count - 1
		strThisKey = arrKeys (intLoop)
		dS = dS & strThisKey & " "
	next

	dim dds
	dds = split(ds," ")
	for i = 0 to ubound(dds)
		for j = 0 to ubound(dds)-1
			if(dds(j) > dds(j+1))then
				s3 = dds(j)
				dds(j) = dds(j+1)
				dds(j+1) = s3
			end if
		next
	next
	
	for i = 0 to ubound(dds)
		wl = wl & dds(i) & vbcrlf
	next	
end if

if(request("WHITELIST") = "WHITELIST")then
	wl = request("LIST")
	if(wl <> "")then
    	SendMail "[C] ADDWHITELIST BODY", wl
	end if
	wl = ""
end if

if(request("BLACKLIST") = "BLACKLIST")then
	wl = request("LIST")
	if(wl <> "")then
    	SendMail "[C] ADDBLACKLIST BODY", wl
	end if
	wl = ""
end if

if(request("GREENLIST") = "GREENLIST")then
	wl = request("LIST")
	if(wl <> "")then
    	SendMail "[C] ADDGREENLIST BODY", wl
	end if
	wl = ""
end if

if(request("WHITEIP") = "WHITEIP")then
	bl = request("LINE")
	if(bl <> "")then
    	SendMail "[C] ADDWHITEIP " & bl, bl
	end if
	bl = ""
end if

if(request("DELWIP") = "DELWIP")then
	bl = request("LINE")
	if(bl <> "")then
    	SendMail "[C] DELETEWHITEIP " & bl, bl
	end if
	bl = ""
end if

if(request("RELBIP") = "RELBIP")then
	bl = request("LINE")
	if(bl <> "")then
    	SendMail "[C] RELEASEBLACKIP " & bl, bl
	end if
	bl = ""
end if

if(request("RELBIR") = "RELBIR")then
	bl = request("LINE")
	if(bl <> "")then
    	SendMail "[C] RELEASEBLACKIPRANGE " & bl, bl
	end if
	bl = ""
end if

if(request("BLACKIP") = "BLACKIP")then
	bl = request("LINE")
	if(bl <> "")then
    	SendMail "[C] ADDBLACKIP " & bl, bl
	end if
	bl = ""
end if

if(request("BLACKIPRANGE") = "BLACKIPRANGE")then
	br = request("LIST")
	bl = request("LINE")
	if(bl = "")then
		bl = "BODY"
	end if
	if((br <> "") and (br <> "000.000.000.000 255.255.255.255 EU"))then
    	SendMail "[C] ADDBLACKIPRANGE " & bl, br
	end if
	bl = ""
end if

%>
<script language="javascript">
   
    function whatToDo() 
    { 
		window.open('', '_parent', '');
		window.close();
    }
    
    function Run()
    {
    	lWidth = 0 ;
    	lHeight = screen.availHeight - 170 ;
    
    	windowMailKeeper = 'toolbar=no,location=no,resizable=yes,directories=no,menubar=no,';
    	windowMailKeeper += 'scrollbars=no,fullscreen=no,height=170,width=500,top=780,left=0';
    		
    	floater = window.open( "https://webmail.cbcm.org/mailkeeper/whitelist.asp", "MailKeeper", windowMailKeeper );
    	floater.moveTo( lWidth, lHeight );
    }
    
    if(document.body.offsetWidth > 504)
    {
    	window.setTimeout("Run()", 0);
    	myTimer=setTimeout("whatToDo()",100) 
    }
</script>

<form action="whitelist.asp" method="post">
<textarea name="LIST" cols="55" rows="5"><%=wl%></textarea><br>
<input type="submit" name="EXTRACT" value="EXTRACT">
<input type="submit" name="WHITELIST" value="WHITELIST">
<input type="submit" name="BLACKLIST" value="BLACKLIST">
<input type="submit" name="BLACKIPRANGE" value="BLACKIPRANGE"><br>
<input type="text" name="LINE" id="LINE">
<input type="submit" name="BLACKIP" value="BLACKIP">&nbsp;&nbsp;
<input type="submit" name="WHITEIP" value="WHITEIP">
<input type="submit" name="GREENLIST" value="GREENLIST">

</form>
</body>
</html>
