#include "vkapp.h"



vkapp::vkapp() {
}


vkapp::~vkapp() {
}

VkResult vkapp::init(VkApplicationInfo appInfo) {
	VkResult result = VK_SUCCESS;
	VkInstanceCreateInfo instanceCreateInfo = { };
	instanceCreateInfo.sType = VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO;
	instanceCreateInfo.pApplicationInfo = &appInfo;

	result = vkCreateInstance(&instanceCreateInfo, nullptr, &m_instance);
	if (result == VK_SUCCESS) {
		uint32_t physicalDeviceCount = 0;
		vkEnumeratePhysicalDevices(m_instance, &physicalDeviceCount, nullptr);

		if (result == VK_SUCCESS) {
			m_physicalDevices.resize(physicalDeviceCount);
			vkEnumeratePhysicalDevices(m_instance, &physicalDeviceCount, &m_physicalDevices[0]);
			vkGetPhysicalDeviceProperties(m_physicalDevices[0], &m_deviceProperties);
			
		}
	}
	return result;
}