#
#cdmaDevTerm sample carrier flashing ironPython script
#copyright 2012 DG, chromableedstudios.com
#
mdn = "5551234455"
min = "4445553333"
user = mdn + "@mycricket.com"
password = "cricket"

cdmaTerm.Connect(phone.ComPortName)#selected port in cdmaDevTerm
cdmaTerm.SendSpc("000000")
cdmaTerm.WriteMdn(mdn)
cdmaTerm.WriteMin(min)
cdmaTerm.WriteEvdo(user,password)
q.Run()
cdmaTerm.Disconnect()


