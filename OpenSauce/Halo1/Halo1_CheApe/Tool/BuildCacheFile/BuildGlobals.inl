/*
	Yelo: Open Sauce SDK
		Halo 1 (Editing Kit) Edition

	See license\OpenSauce\Halo1_CheApe for specific license information
*/


struct s_build_cache_file_for_scenario {
	bool build_failed;
	PAD24;
	cstring scenario_path;

	typedef bool (PLATFORM_API* _data_file_open)(_enum type, cstring name, bool writable);
	typedef bool (PLATFORM_API* _data_file_close)(_enum type);

	cstring*			data_file_open_path_format;	// address which references "maps\\%s.map"
	_data_file_open		data_file_open;
	_data_file_close	data_file_close;

	Cache::s_build_cache_file_globals* globals;

	typedef bool (PLATFORM_API* _build_cache_file_cull_tags)();
	typedef bool (PLATFORM_API* _build_structure_bsp_predicted_resources)();
	typedef bool (PLATFORM_API* _build_cache_file_add_tags)(Cache::s_cache_header& header, void* scratch_buffer, datum_index tag_indexes[], int32 prior_tag_memory_space_used);
	typedef void (PLATFORM_API* __build_cache_file_for_scenario)(cstring scenario_name);
	typedef bool (PLATFORM_API* _build_cache_file_begin)(cstring scenario_name);
	typedef bool (PLATFORM_API* _build_cache_file_end)(Cache::s_cache_header* header);
	typedef void (PLATFORM_API* _build_cache_file_failed)();
	typedef bool (PLATFORM_API* _build_cache_file_add_resource)(const void* buffer, size_t buffer_size, uint32* out_file_offset, bool update_crc);

	_build_cache_file_cull_tags		build_cache_file_cull_tags;
	_build_structure_bsp_predicted_resources build_structure_bsp_predicted_resources;
	_build_cache_file_add_tags		build_cache_file_add_tags;
	__build_cache_file_for_scenario	_build_cache_file_for_scenario;
	proc_import_class				build_cache_file_for_scenario_command;
	_build_cache_file_begin			build_cache_file_begin;
	_build_cache_file_end			build_cache_file_end;
	_build_cache_file_failed		build_cache_file_failed;
	_build_cache_file_add_resource	build_cache_file_add_resource;

	void*		build_cache_file_add_structure_bsps;	// address of the build_cache_file_add_structure_bsps (requires wrapper due to non-stack parameters)
	void*		build_cache_file_write;					// address of the build_cache_file_write function (requires wrapper due to non-stack parameters)
	void*		build_cache_file_end_sprintf_call;		// address of the call to sprintf for building the output map file path
	cstring*	build_cache_file_output_path_format;	// address which references "%s%s%s.map"
	char*		build_cache_file_output_directory;		static const size_t k_build_cache_file_output_directory_size = 256;


	static bool AddStructureBsps(void* scratch_buffer, datum_index tag_indexes[], int32& out_largest_bsp_size);
	static void Write(void* buffer, size_t buffer_size, int32* out_file_offset);

	// Initialize the cache and data_file file systems
	static void InitializeFileSystem(Cache::c_data_files& data_files, 
		cstring mod_name, bool using_mod_sets);

	void DataFilesOpen(const Cache::c_data_files& data_files, bool store_resources);
	void DataFilesClose(bool store_resources);
	void BuildPostprocess(cstring mod_name, bool using_mod_sets);

	// Forces tool to use our own sprintf-like function which uses the OS cache file naming convention
	void InitializeBuildCacheFileEndSprintfOverride();
	static void __cdecl BuildCacheFileEndSprintfOverride(char* buffer, cstring format, 
		cstring output_directory, cstring map_dir, cstring scenario_name);

	// Force Tool to use the Yelo data_file path scheme
	// If the user has defined their maps directory in the settings, this will use it.
	void UseYeloDataFilePathScheme();	// "maps\data_files\"
	// Force Tool to use the Yelo map file naming scheme
	void UseYeloMapNamingScheme();		// "<map-name>.yelo"

}build_cache_file_for_scenario_internals = {
	false,
	NULL,

	CAST_PTR(cstring*,															0x4B9FB6),
	CAST_PTR(s_build_cache_file_for_scenario::_data_file_open,					0x4B9F10),
	CAST_PTR(s_build_cache_file_for_scenario::_data_file_close,					0x4BA100),

	CAST_PTR(Cache::s_build_cache_file_globals*,								0x10A1030),

	CAST_PTR(s_build_cache_file_for_scenario::_build_cache_file_cull_tags,		0x453260),
	CAST_PTR(s_build_cache_file_for_scenario::_build_structure_bsp_predicted_resources,		0x453860),
	CAST_PTR(s_build_cache_file_for_scenario::_build_cache_file_add_tags,		0x454D40),
	CAST_PTR(s_build_cache_file_for_scenario::__build_cache_file_for_scenario,	0x4553A0),
	CAST_PTR(proc_import_class,													0x455640),
	CAST_PTR(s_build_cache_file_for_scenario::_build_cache_file_begin,			0x4B9250),
	CAST_PTR(s_build_cache_file_for_scenario::_build_cache_file_end,			0x4B93B0),
	CAST_PTR(s_build_cache_file_for_scenario::_build_cache_file_failed,			0x4B9030),
	CAST_PTR(s_build_cache_file_for_scenario::_build_cache_file_add_resource,	0x4B9350),

	CAST_PTR(void*,																0x454B70),
	CAST_PTR(void*,																0x4B9180),
	CAST_PTR(void*,																0x4B944D),
	CAST_PTR(cstring*,															0x4B9448),
	CAST_PTR(char*,																0x10FD510),
};



API_FUNC_NAKED bool s_build_cache_file_for_scenario::AddStructureBsps(void* scratch_buffer, datum_index tag_indexes[], int32& out_largest_bsp_size)
{
	API_FUNC_NAKED_START()
		push	out_largest_bsp_size
		push	tag_indexes
		mov		eax, scratch_buffer
		call	build_cache_file_for_scenario_internals.build_cache_file_add_structure_bsps
		add		esp, 4 * 2
	API_FUNC_NAKED_END(3);
}
API_FUNC_NAKED void s_build_cache_file_for_scenario::Write(void* buffer, size_t buffer_size, int32* out_file_offset)
{
	API_FUNC_NAKED_START()
		mov		edi, buffer_size
		mov		ecx, buffer
		call	build_cache_file_for_scenario_internals.build_cache_file_write
		add		esp, 4 * 1
	API_FUNC_NAKED_END(3);
}

void s_build_cache_file_for_scenario::InitializeFileSystem(Cache::c_data_files& data_files, 
														   cstring mod_name, bool using_mod_sets)
{
	cstring settings_path = Settings::Get().GetMapsPath();

	char maps_path[MAX_PATH];
	strcpy_s(maps_path, settings_path);
	_mkdir(maps_path);

	data_files.InitializeForCache(using_mod_sets, mod_name, maps_path);

	if(using_mod_sets)
		build_cache_file_for_scenario_internals.UseYeloDataFilePathScheme();
}

void s_build_cache_file_for_scenario::DataFilesOpen(const Cache::c_data_files& data_files, bool store_resources)
{
	data_file_open(Enums::_data_file_type_bitmaps+1,data_files.m_names[Enums::_data_file_type_bitmaps],store_resources);
	data_file_open(Enums::_data_file_type_sounds+1,	data_files.m_names[Enums::_data_file_type_sounds], store_resources);
	data_file_open(Enums::_data_file_type_locale+1,	data_files.m_names[Enums::_data_file_type_locale], store_resources);

	if(store_resources)
		globals->DataFilesPreprocessForSave();
}

void s_build_cache_file_for_scenario::DataFilesClose(bool store_resources)
{
	if(store_resources)
		globals->DataFilesSave();

	data_file_close(Enums::_data_file_type_bitmaps+1);
	data_file_close(Enums::_data_file_type_sounds+1);
	data_file_close(Enums::_data_file_type_locale+1);
}

void s_build_cache_file_for_scenario::BuildPostprocess(cstring mod_name, bool using_mod_sets)
{
	if(!using_mod_sets) return;

	if(!build_failed)
	{
		errno_t err = Yelo::DataFiles::AddFileSet(mod_name);

		switch(err)
		{
		case EEXIST:
			printf_s("Engine Mod: '%s' already exist in Yelo mod database.\n", mod_name);
			break;

		case ENOSPC:
			printf_s("Engine Mod: ran out of mod slots, '%s' wasn't automatically added.\n", mod_name);
			break;

		default:
			printf_s("'%s' added to Yelo mod database.\n", mod_name);
			Yelo::DataFiles::SaveSettings();
			break;
		}
	}
	else
		printf_s("Engine Mod: build failed, '%s' wasn't added to Yelo mod database.\n", mod_name);
}

void s_build_cache_file_for_scenario::InitializeBuildCacheFileEndSprintfOverride()
{
	Memory::WriteRelativeCall(BuildCacheFileEndSprintfOverride, build_cache_file_end_sprintf_call);
}

void __cdecl s_build_cache_file_for_scenario::BuildCacheFileEndSprintfOverride(
	char* buffer, 
	cstring format,				// same pointer as [build_cache_file_output_path_format]
	cstring output_directory,	// same pointer as [build_cache_file_output_directory]
	cstring map_dir, cstring scenario_name)
{
	sprintf(buffer, "%s%s.yelo", Settings::Get().GetMapsPath(), scenario_name);
}
void s_build_cache_file_for_scenario::UseYeloDataFilePathScheme()
{
	static char data_file_path_format[MAX_PATH];

	strcpy_s(data_file_path_format, Settings::Get().GetMapsPath());
	strcat_s(data_file_path_format, "%s.map");

	*data_file_open_path_format = data_file_path_format;
}
void s_build_cache_file_for_scenario::UseYeloMapNamingScheme()
{
	*build_cache_file_output_path_format = "%s%s%s.yelo";
}