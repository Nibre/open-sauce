/*
	Yelo: Open Sauce SDK
		Halo 1 (CE) Edition

	See license\OpenSauce\Halo1_CE for specific license information
*/
#pragma once

#include <blamlib/Halo1/memory/data.hpp>

namespace Yelo
{
	namespace Lights
	{
		struct s_lights_datum : TStructImpl(124)
		{
		};
		typedef Memory::DataArray<s_lights_datum, 896> t_lights_data;
		t_lights_data*					Lights();


		struct s_lights_globals_data
		{
			bool enabled;
			PAD24;
		};
		s_lights_globals_data*			LightsGlobals();

		struct s_light_cluster_data : TStructImpl(2048)
		{
		};
		s_light_cluster_data*				LightCluster();


		struct s_cluster_light_reference_datum : TStructImpl(12)
		{
		};
		typedef Memory::DataArray<s_cluster_light_reference_datum, 2048> t_cluster_light_reference_data;
		t_cluster_light_reference_data*	ClusterLightReference();


		struct s_light_cluster_reference_datum : TStructImpl(12)
		{
		};
		typedef Memory::DataArray<s_light_cluster_reference_datum, 2048> t_light_cluster_reference_data;
		t_light_cluster_reference_data*	LightClusterReference();
	};
};