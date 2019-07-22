Public Class SchedulerModel
	Public Property ActiveHoursStart As Integer
	Public Property ActiveHoursEnd As Integer
	Public Property Appointments As New List(Of AppointmentModel)
	Public Property Users As New List(Of UserModel)

	''' <summary>
	''' Receives the start and end hours for flexibility. 
	''' </summary>
	''' <param name="_ActiveHoursStart"></param>
	''' <param name="_ActiveHoursEnd"></param>
	Public Sub New(_ActiveHoursStart As Integer, _ActiveHoursEnd As Integer)
		ActiveHoursStart = _ActiveHoursStart
		ActiveHoursEnd = _ActiveHoursEnd
	End Sub
End Class
