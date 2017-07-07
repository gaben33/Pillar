using System;
using System.Collections.Generic;
using Pillar3D;

public class SampleComponent : Component {
	
	public SampleComponent() : base(true) {
		Rails.Update += Update;
	}
	
	public void Update () {
		
	}
}
