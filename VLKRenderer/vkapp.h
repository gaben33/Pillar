#pragma once
#include "vulkan\vulkan.h"
#include <stdlib.h>
#include <vector>
class vkapp {
public:
	vkapp();
	~vkapp();
	VkResult init(VkApplicationInfo appInfo);
private:
	VkInstance m_instance;
	std::vector<VkPhysicalDevice> m_physicalDevices;
	VkPhysicalDeviceProperties m_deviceProperties;
};

