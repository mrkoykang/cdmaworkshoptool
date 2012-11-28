# sample carrier(cricket) flashing
# cdmaDevTerm ironPython script
# copyright 2012 dg, chromableedstudios.com
#
mdn = "0000000000"
min = "0000000000"
user = mdn + "@mycricket.com"
password = "cricket"

cdmaTerm.Connect(phone.ComPortName)#selected port in cdmaDevTerm
cdmaTerm.ModeSwitch(Qcdm.Mode.MODE_RADIO_OFFLINE)
q.Run()
cdmaTerm.SendSpc("000000")
#cdmaTerm.UnlockMotoEvdo() #uncomment for moto nvitem 8035
cdmaTerm.WriteMdn(mdn)
cdmaTerm.WriteMin(min)
cdmaTerm.WriteEvdo(user,password)
q.Run()
cdmaTerm.ModeSwitch(Qcdm.Mode.MODE_RADIO_RESET)
q.Run()
cdmaTerm.Disconnect()


