Public Interface ISchedulerService
	Function GetSchedulerActiveHours() As Integer()
	Function GetAppointments() As List(Of AppointmentModel)
	Function CreateAppointment(appointmentData As AppointmentModel) As AppointmentModel
End Interface
