using System;
using System.Collections.Generic;
using Pillar3D;

public class SampleComponent : Component {

	public SampleComponent() : base(true) {
		base.Update += Update;
		Container.OnLevelChanged += Rebase;
	}

	new public void Update() {

	}

	private void Rebase () {
		Container.ContainerLevel.Rail.Update -= Update;
	}
}