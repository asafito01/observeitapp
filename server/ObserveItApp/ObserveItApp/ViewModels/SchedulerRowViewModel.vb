''' <summary>
''' A view model data type for the controller to consume
''' </summary>
Public Class SchedulerRowViewModel
	Public Property StartHour As TimeSpan
	Public Property EndHour As TimeSpan
	Public Property Participants As New List(Of String)
	Public ReadOnly Property ParticipantsCount As Integer
		Get
			If Participants IsNot Nothing Then Return Participants.Count
			Return 0
		End Get
	End Property
End Class
