private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressValue(trackBar.Value, trackBar.Maximum);
        }

        private void btnNoProgress_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        private void btnIndeterminate_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.Indeterminate);
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.Normal);
            TaskbarManager.SetProgressValue(trackBar.Value, trackBar.Maximum);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.Error);
            TaskbarManager.SetProgressValue(trackBar.Value, trackBar.Maximum);
        }

        private void btnPaused_Click(object sender, EventArgs e)
        {
            TaskbarManager.SetProgressState(TaskbarProgressBarState.Paused);
            TaskbarManager.SetProgressValue(trackBar.Value, trackBar.Maximum);
        }