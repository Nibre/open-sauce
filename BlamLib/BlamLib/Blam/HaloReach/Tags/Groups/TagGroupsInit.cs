/*
	BlamLib: .NET SDK for the Blam Engine

	See license\BlamLib\BlamLib for specific license information
*/
﻿using System;
using BlamLib.TagInterface;

namespace BlamLib.Blam.HaloReach
{
	partial class TagGroups
	{
		static TagGroups()
		{
			GroupsInitialize();

// 			obje.Definition = new Tags.object_group().State;
// 			devi.Definition = new Tags.device_group().State;
// 			item.Definition = new Tags.item_group().State;
// 			unit.Definition = new Tags.unit_group().State;
// 			rm__.Definition = new Tags.render_method_group().State;


// 			_rmc.Definition = new Tags.shader_contrail_group().State;
// 			_rmp.Definition = new Tags.shader_particle_group().State;
 			//shit.Definition = new Tags.cache_file_sound_group().State;
// 			srscen.Definition = new Tags.scenario_scenery_resource_group().State;
// 			srweap.Definition = new Tags.scenario_weapons_resource_group().State;
// 			srvehi.Definition = new Tags.scenario_vehicles_resource_group().State;
// 			srefsc.Definition = new Tags.scenario_effect_scenery_resource_group().State;
// 			srligh.Definition = new Tags.scenario_lights_resource_group().State;
// 			srbipd.Definition = new Tags.scenario_bipeds_resource_group().State;
// 			sreqip.Definition = new Tags.scenario_equipment_resource_group().State;
// 			srcrea.Definition = new Tags.scenario_creature_resource_group().State;
// 			srssce.Definition = new Tags.scenario_sound_scenery_resource_group().State;
// 			srcmmt.Definition = new Tags.scenario_comments_resource_group().State;
// 			_fx_.Definition = new Tags.sound_effect_template_group().State;
// 			BooM.Definition = new Tags.stereo_system_group().State;
			//LMgS.Definition = new Tags.lightmapper_globals_group().State;
			//Lbsp.Definition = new Tags.scenario_lightmap_bsp_data_group().State;
			//ZZAP.Definition = new Tags.lightning_system_group().State;
			//achi.Definition = new Tags.achievements_group().State;
// 			adlg.Definition = new Tags.ai_dialogue_globals_group().State;
// 			srai.Definition = new Tags.scenario_ai_resource_group().State;
			//aigl.Definition = new Tags.ai_globals_group().State;
			//airs.Definition = new Tags.airstrike_group().State;
// 			ant_.Definition = new Tags.antenna_group().State;
			//atgf.Definition = new Tags.atmosphere_globals_group().State;
			//avat.Definition = new Tags.avatar_awards_group().State;
			//bbcr.Definition = new Tags.big_battle_creature_group().State;
			//bdpd.Definition = new Tags.death_program_selector_group().State;
// 			beam.Definition = new Tags.beam_system_group().State;
// 			bink.Definition = new Tags.Bink_group().State;
// 			bipd.Definition = new Tags.biped_group().State;
// 			bitm.Definition = new Tags.bitmap_group().State;
// 			bloc.Definition = new Tags.crate_group().State;
// 			bsdt.Definition = new Tags.breakable_surface_group().State;
// 			cddf.Definition = new Tags.collision_damage_group().State;
// 			cfxs.Definition = new Tags.camera_fx_settings_group().State;
// 			chad.Definition = new Tags.chud_animation_definition_group().State;
// 			char_.Definition = new Tags.character_group().State;
			//chdg.Definition = new Tags.challenge_globals_definition_group().State;
// 			chdt.Definition = new Tags.chud_definition_group().State;
// 			chgd.Definition = new Tags.chud_globals_definition_group().State;
// 			chmt.Definition = new Tags.chocolate_mountain_new_group().State;
// 			srcine.Definition = new Tags.scenario_cinematics_resource_group().State;
// 			cine.Definition = new Tags.cinematic_group().State;
// 			cisc.Definition = new Tags.cinematic_scene_group().State;
			//citr.Definition = new Tags.cinematic_transition_group().State;
// 			srclut.Definition = new Tags.scenario_cluster_data_resource_group().State;
// 			clwd.Definition = new Tags.cloth_group().State;
			//cmoe.Definition = new Tags.camo_group().State;
// 			cntl.Definition = new Tags.contrail_system_group().State;
// 			coll.Definition = new Tags.collision_model_group().State;
// 			colo.Definition = new Tags.color_table_group().State;
			//comg.Definition = new Tags.commendation_globals_definition_group().State;
			//coms.Definition = new Tags.communication_sounds_group().State;
			//cook.Definition = new Tags.cookie_globals_definition_group().State;
			//coop.Definition = new Tags.coop_spawning_globals_definition_group().State;
			//cpem.Definition = new Tags.cheap_particle_emitter_group().State;
			//cpgd.Definition = new Tags.cookie_purchase_globals_group().State;
			//cptl.Definition = new Tags.cheap_particle_type_library_group().State;
// 			crea.Definition = new Tags.creature_group().State;
			//csdt.Definition = new Tags.camera_shake_group().State;
// 			ctrl.Definition = new Tags.device_control_group().State;
// 			srcube.Definition = new Tags.scenario_cubemap_resource_group().State;
			//cusc.Definition = new Tags.cui_screen_group().State;
			//cust.Definition = new Tags.cui_static_data_group().State;
// 			srdcrs.Definition = new Tags.scenario_decorators_resource_group().State;
// 			dctr.Definition = new Tags.decorator_set_group().State;
// 			srdeca.Definition = new Tags.scenario_decals_resource_group().State;
// 			decs.Definition = new Tags.decal_system_group().State;
// 			devo.Definition = new Tags.cellular_automata_group().State;
// 			srdgrp.Definition = new Tags.scenario_devices_resource_group().State;
			//srdmpr.Definition = new Tags.scenario_dumplings_resource_group().State;
// 			dobc.Definition = new Tags.detail_object_collection_group().State;
// 			draw.Definition = new Tags.rasterizer_cache_file_globals_group().State;
// 			drdf.Definition = new Tags.damage_response_definition_group().State;
			//ebhd.Definition = new Tags.particle_emitter_boat_hull_shape_group().State;
// 			effe.Definition = new Tags.effect_group().State;
// 			effg.Definition = new Tags.effect_globals_group().State;
// 			efsc.Definition = new Tags.effect_scenery_group().State;
// 			eqip.Definition = new Tags.equipment_group().State;
// 			flck.Definition = new Tags.flock_group().State;
// 			fldy.Definition = new Tags.fluid_dynamics_group().State;
			//fogg.Definition = new Tags.atmosphere_fog_group().State;
// 			foot.Definition = new Tags.material_effects_group().State;
			//form.Definition = new Tags.formation_group().State;
			//frms.Definition = new Tags.frame_event_list_group().State;
			//fxtt.Definition = new Tags.fx_test_group().State;
			//gcrg.Definition = new Tags.game_completion_rewards_globals_group().State;
			//gegl.Definition = new Tags.game_engine_globals_group().State;
// 			gint.Definition = new Tags.giant_group().State;
			//gldf.Definition = new Tags.cheap_light_group().State;
// 			glps.Definition = new Tags.global_pixel_shader_group().State;
// 			glvs.Definition = new Tags.global_vertex_shader_group().State;
			//gmeg.Definition = new Tags.game_medal_globals_group().State;
// 			goof.Definition = new Tags.multiplayer_variant_settings_interface_definition_group().State;
			//gpix.Definition = new Tags.global_cache_file_pixel_shaders_group().State;
			//gptd.Definition = new Tags.game_performance_throttle_group().State;
			//grfr.Definition = new Tags.grounded_friction_group().State;
			//hcfd.Definition = new Tags.havok_collision_filter_group().State;
// 			hlmt.Definition = new Tags.model_group().State;
// 			hlsl.Definition = new Tags.hlsl_include_group().State;
// 			srhscf.Definition = new Tags.scenario_hs_source_file_group().State;
			//igpd.Definition = new Tags.incident_global_properties_definition_group().State;
			//iimz.Definition = new Tags.instance_imposter_definition_group().State;
			//impo.Definition = new Tags.imposter_model_group().State;
			//ingd.Definition = new Tags.incident_globals_definition_group().State;
// 			jmad.Definition = new Tags.model_animation_graph_group().State;
// 			jmrq.Definition = new Tags.sandbox_text_value_pair_definition_group().State;
// 			jpt_.Definition = new Tags.damage_effect_group().State;
			//ldsc.Definition = new Tags.load_screen_group().State;
// 			lens.Definition = new Tags.lens_flare_group().State;
			//lgtd.Definition = new Tags.loadout_globals_definition_group().State;
// 			ligh.Definition = new Tags.light_group().State;
			//locs.Definition = new Tags.location_name_globals_definition_group().State;
// 			lsnd.Definition = new Tags.sound_looping_group().State;
// 			lswd.Definition = new Tags.leaf_system_group().State;
// 			ltvl.Definition = new Tags.light_volume_system_group().State;
// 			mach.Definition = new Tags.device_machine_group().State;
// 			matg.Definition = new Tags.globals_group().State;
// 			mdlg.Definition = new Tags.ai_mission_dialogue_group().State;
// 			metr.Definition = new Tags.meter_group().State;
// 			mffn.Definition = new Tags.muffin_group().State;
			//mgls.Definition = new Tags.megalogamengine_sounds_group().State;
			//mlib.Definition = new Tags.emblem_library_group().State;
// 			mode.Definition = new Tags.render_model_group().State;
			//motl.Definition = new Tags.multiplayer_object_type_list_group().State;
// 			mply.Definition = new Tags.multiplayer_scenario_description_group().State;
			//msit.Definition = new Tags.megalo_string_id_table_group().State;
// 			mulg.Definition = new Tags.multiplayer_globals_group().State;
			//muxg.Definition = new Tags.mux_generator_group().State;
// 			nclt.Definition = new Tags.new_cinematic_lighting_group().State;
			//pach.Definition = new Tags.tag_package_manifest_group().State;
			//pcec.Definition = new Tags.pgcr_enemy_to_category_mapping_definition_group().State;
			//pecp.Definition = new Tags.particle_emitter_custom_points_group().State;
// 			perf.Definition = new Tags.performance_throttles_group().State;
			//pfmc.Definition = new Tags.performance_template_group().State;
			//pfpt.Definition = new Tags.planar_fog_parameters_group().State;
			//pggd.Definition = new Tags.player_grade_globals_definition_group().State;
// 			phmo.Definition = new Tags.physics_model_group().State;
// 			pixl.Definition = new Tags.pixel_shader_group().State;
 			//play.Definition = new Tags.cache_file_resource_layout_table_group().State;
			//pmcg.Definition = new Tags.player_model_customization_globals_group().State;
// 			pmdf.Definition = new Tags.particle_model_group().State;
// 			pmov.Definition = new Tags.particle_physics_group().State;
// 			pphy.Definition = new Tags.point_physics_group().State;
			//srprfr.Definition = new Tags.scenario_performances_resource_group().State;
// 			proj.Definition = new Tags.projectile_group().State;
// 			prt3.Definition = new Tags.particle_group().State;
			//rain.Definition = new Tags.rain_definition_group().State;
// 			rasg.Definition = new Tags.rasterizer_globals_group().State;
// 			rmb_.Definition = new Tags.shader_beam_group().State;
			//rmbl.Definition = new Tags.rumble_group().State;
// 			rmcs.Definition = new Tags.shader_custom_group().State;
// 			rmct.Definition = new Tags.shader_cortana_group().State;
// 			rmd_.Definition = new Tags.shader_decal_group().State;
// 			rmdf.Definition = new Tags.render_method_definition_group().State;
// 			rmfl.Definition = new Tags.shader_foliage_group().State;
			//rmfs.Definition = new Tags.shader_fur_stencil_group().State;
			//rmfu.Definition = new Tags.shader_fur_group().State;
			//rmgl.Definition = new Tags.shader_glass_group().State;
// 			rmhg.Definition = new Tags.shader_halogram_group().State;
// 			rmlv.Definition = new Tags.shader_light_volume_group().State;
			//rmmm.Definition = new Tags.shader_mux_material_group().State;
			//rmmx.Definition = new Tags.shader_mux_group().State;
// 			rmop.Definition = new Tags.render_method_option_group().State;
// 			rmsh.Definition = new Tags.shader_group().State;
// 			rmsk.Definition = new Tags.shader_skin_group().State;
			//rmss.Definition = new Tags.shader_screen_group().State;
// 			rmt2.Definition = new Tags.render_method_template_group().State;
// 			rmtr.Definition = new Tags.shader_terrain_group().State;
// 			rmw_.Definition = new Tags.shader_water_group().State;
// 			rwrd.Definition = new Tags.render_water_ripple_group().State;
// 			sFdT.Definition = new Tags.scenario_faux_data_group().State;
// 			sLdT.Definition = new Tags.scenario_lightmap_group().State;
			//sadt.Definition = new Tags.spring_acceleration_group().State;
 			//sbsp.Definition = new Tags.scenario_structure_bsp_group().State;
// 			scen.Definition = new Tags.scenery_group().State;
			//scmb.Definition = new Tags.sound_combiner_group().State;
// 			scnr.Definition = new Tags.scenario_group().State;
// 			sddt.Definition = new Tags.structure_design_group().State;
			//sdzg.Definition = new Tags.scenario_required_resource_group().State;
// 			sefc.Definition = new Tags.area_screen_effect_group().State;
// 			sfx_.Definition = new Tags.sound_effect_collection_group().State;
// 			sgp_.Definition = new Tags.sound_global_propagation_group().State;
// 			shit2.Definition = new Tags.shield_impact_group().State;
			//sidt.Definition = new Tags.simulated_input_group().State;
			//siin.Definition = new Tags.simulation_interpolation_group().State;
// 			sily.Definition = new Tags.text_value_pair_definition_group().State;
			//sirp.Definition = new Tags.sound_incident_response_group().State;
// 			srsky_.Definition = new Tags.scenario_sky_references_resource_group().State;
// 			skya.Definition = new Tags.sky_atm_parameters_group().State;
// 			smap.Definition = new Tags.shared_cache_file_layout_group().State;
			//smdt.Definition = new Tags.survival_mode_globals_group().State;
// 			sncl.Definition = new Tags.sound_classes_group().State;
// 			snd_.Definition = new Tags.sound_group().State;
// 			snde.Definition = new Tags.sound_environment_group().State;
// 			snmx.Definition = new Tags.sound_mix_group().State;
// 			spk_.Definition = new Tags.sound_dialogue_constants_group().State;
			//sqtm.Definition = new Tags.squad_template_group().State;
			//srad.Definition = new Tags.sound_radio_settings_group().State;
			//ssao.Definition = new Tags.ssao_definition_group().State;
// 			ssce.Definition = new Tags.sound_scenery_group().State;
// 			srsslt.Definition = new Tags.scenario_structure_lighting_resource_group().State;
// 			stli.Definition = new Tags.scenario_structure_lighting_info_group().State;
// 			stse.Definition = new Tags.structure_seams_group().State;
// 			styl.Definition = new Tags.style_group().State;
// 			term.Definition = new Tags.device_terminal_group().State;
// 			trak.Definition = new Tags.camera_track_group().State;
// 			srtrgr.Definition = new Tags.scenario_trigger_volumes_resource_group().State;
			//ttag.Definition = new Tags.test_tag_group().State;
// 			udlg.Definition = new Tags.dialogue_group().State;
 			//ugh_.Definition = new Tags.sound_cache_file_gestalt_group().State;
// 			uise.Definition = new Tags.user_interface_sounds_definition_group().State;
// 			unic.Definition = new Tags.multilingual_unicode_string_list_group().State;
			//uttt.Definition = new Tags.tag_template_unit_test_group().State;
// 			vehi.Definition = new Tags.vehicle_group().State;
			//vmdx.Definition = new Tags.vision_mode_group().State;
			//vtgl.Definition = new Tags.variant_globals_group().State;
// 			vtsh.Definition = new Tags.vertex_shader_group().State;
			//wadt.Definition = new Tags.chud_widget_animation_data_template_group().State;
			//wave.Definition = new Tags.wave_template_group().State;
			//wdst.Definition = new Tags.chud_widget_datasource_template_group().State;
// 			weap.Definition = new Tags.weapon_group().State;
			//wetn.Definition = new Tags.scenario_wetness_bsp_data_group().State;
// 			wezr.Definition = new Tags.game_engine_settings_definition_group().State;
// 			wgtz.Definition = new Tags.user_interface_globals_definition_group().State;
// 			whip.Definition = new Tags.cellular_automata2d_group().State;
// 			wigl.Definition = new Tags.user_interface_shared_globals_definition_group().State;
// 			wind.Definition = new Tags.Wind_group().State;
			//wpdp.Definition = new Tags.water_physics_drag_properties_group().State;
			//wpdt.Definition = new Tags.chud_widget_placement_data_template_group().State;
			//wrdt.Definition = new Tags.chud_widget_render_data_template_group().State;
			//wsdt.Definition = new Tags.chud_widget_state_data_template_group().State;
			//wxcg.Definition = new Tags.weather_globals_group().State;
			//zone.Definition = new Tags.cache_file_resource_gestalt_group().State;

			#region Beta only
// 			bkey.Definition = new Tags.gui_button_key_definition_group().State;
// 			bmp3.Definition = new Tags.gui_bitmap_widget_definition_group().State;
// 			dsrc.Definition = new Tags.gui_datasource_definition_group().State;
// 			grup.Definition = new Tags.gui_group_widget_definition_group().State;
// 			lst3.Definition = new Tags.gui_list_widget_definition_group().State;
// 			mdl3.Definition = new Tags.gui_model_widget_definition_group().State;
// 			scn3.Definition = new Tags.gui_screen_widget_definition_group().State;
// 			skn3.Definition = new Tags.gui_skin_definition_group().State;
// 			txt3.Definition = new Tags.gui_text_widget_definition_group().State;
// 			wacd.Definition = new Tags.gui_widget_animation_collection_definition_group().State;
// 			wclr.Definition = new Tags.gui_widget_color_animation_definition_group().State;
// 			wfon.Definition = new Tags.gui_widget_font_animation_definition_group().State;
// 			wgan.Definition = new Tags.gui_widget_animation_definition_group().State;
// 			wpos.Definition = new Tags.gui_widget_position_animation_definition_group().State;
// 			wrot.Definition = new Tags.gui_widget_rotation_animation_definition_group().State;
// 			wscl.Definition = new Tags.gui_widget_scale_animation_definition_group().State;
// 			wspr.Definition = new Tags.gui_widget_sprite_animation_definition_group().State;
// 			wtuv.Definition = new Tags.gui_widget_texture_coordinate_animation_definition_group().State;
			#endregion

			for (int x = 1; x < Groups.Count; x++)
				Groups[x].InitializeHandle(BlamVersion.HaloReach, x, false);
		}
	};
}