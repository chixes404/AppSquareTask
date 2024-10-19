﻿using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppSquareTask.Application.MediatrHandelr.AdminManagment.ApproveOwner
{
	

	public class ApproveOwnerCommand : IRequest<ApiResponse<string>>
	{
		public int OwnerId { get; set; }
	}

}
