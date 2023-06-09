﻿using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Assignment.Commands;

public record UpdateAssignment(Guid Id, string Name, string Description, int Priority) : ICommand;