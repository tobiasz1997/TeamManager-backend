﻿using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Core.User.Enums;

namespace TeamManager.Application.Assignment.Commands;

public record UpdateAssignmentStatus(Guid Id, AssignmentStatusType Status) : ICommand;