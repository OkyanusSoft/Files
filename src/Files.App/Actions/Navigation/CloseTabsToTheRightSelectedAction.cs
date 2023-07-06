﻿// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

namespace Files.App.Actions
{
	internal class CloseTabsToTheRightSelectedAction : ObservableObject, IAction
	{
		private readonly IMultitaskingContext context;

		public string Label
			=> "CloseTabsToTheRight".GetLocalizedResource();

		public string Description
			=> "CloseTabsToTheRightSelectedDescription".GetLocalizedResource();

		public bool IsExecutable
			=> GetIsExecutable();

		public CloseTabsToTheRightSelectedAction()
		{
			context = Ioc.Default.GetRequiredService<IMultitaskingContext>();

			context.PropertyChanged += Context_PropertyChanged;
		}

		public Task ExecuteAsync()
		{
			if (context.Control is not null)
				MultitaskingTabsHelpers.CloseTabsToTheRight(context.SelectedTabItem, context.Control);

			return Task.CompletedTask;
		}

		private bool GetIsExecutable()
		{
			return context.Control is not null && context.SelectedTabIndex < context.TabCount - 1;
		}

		private void Context_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(IMultitaskingContext.Control):
				case nameof(IMultitaskingContext.TabCount):
				case nameof(IMultitaskingContext.SelectedTabIndex):
					OnPropertyChanged(nameof(IsExecutable));
					break;
			}
		}
	}
}
