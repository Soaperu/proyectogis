from comtypes.client import GetModule, CreateObject
from snippets import GetStandaloneModules, InitStandalone
import arcpy
from bisect import bisect


GetStandaloneModules()
InitStandalone()

install_dir = arcpy.GetInstallInfo()['InstallDir']

def set_scale_properties(mxd_path, name_scale, **kwargs):
    """
    Set the properties of a scale
    :param mxd_path: Path to the mxd file
    :param name_scale: Name of the scale to set
    :param kwargs: Properties to set
    :return:
    """
    global install_dir
    esriCarto = GetModule(r'{}\com\esriCarto.olb'.format(install_dir))
    mxdObject = CreateObject(esriCarto.MapDocument, interface=esriCarto.IMapDocument)
    mxdObject.Open(mxd_path)

    IMap = mxdObject.ActiveView.FocusMap

    for i in xrange(0, IMap.MapSurroundCount):
        element = IMap.MapSurround(i)
        if element.name == name_scale:
            IScaleBar = element.QueryInterface(esriCarto.IScaleBar)
            if kwargs.get('UnitLabel'):
                IScaleBar.UnitLabel = kwargs['UnitLabel']
            if kwargs.get('Division'):
                IScaleBar.Division = kwargs['Division']
            if kwargs.get('Units'):
                IScaleBar.Units = kwargs['Units']
            break
    mxdObject.Save()
    del mxdObject

def select_grid_by_name(mxd_path, name_grid, exclude_grids=None):
    """
    Select a grid by name
    :param mxd_path: Path to the mxd file
    :param name_grid: Name of the grid to select
    :param exclude_grids: List of names of grids to exclude
    :return:
    """
    global install_dir
    esriCarto = GetModule(r'{}\com\esriCarto.olb'.format(install_dir))
    mxdObject = CreateObject(esriCarto.MapDocument, interface=esriCarto.IMapDocument)
    mxdObject.Open(mxd_path)
    activeView = mxdObject.ActiveView
    pageLayout = activeView.QueryInterface(esriCarto.IPageLayout)
    graphicsContainer = pageLayout.QueryInterface(esriCarto.IGraphicsContainer)
    frameElement = graphicsContainer.FindFrame(mxdObject.ActiveView.FocusMap)
    mapFrame = frameElement.QueryInterface(esriCarto.IMapFrame)
    mapGrids = mapFrame.QueryInterface(esriCarto.IMapGrids)
    for i in xrange(mapGrids.MapGridCount):
        grid = mapGrids.MapGrid(i)
        if exclude_grids:
            if grid.Name in exclude_grids:
                continue
        grid.Visible = grid.Name == name_grid
        # print grid.Name
        # print grid.Visible
    mxdObject.Save()
    del mxdObject


def select_grid_by_scale(mxd_path, scale):
    """
    Select a grid by scale
    :param mxd_path: Path to the mxd file
    :param scale: Scale to select
    :return:
    """
    global install_dir

    esriCarto = GetModule(r'{}\com\esriCarto.olb'.format(install_dir))
    mxdObject = CreateObject(esriCarto.MapDocument, interface=esriCarto.IMapDocument)
    mxdObject.Open(mxd_path)

    IMap = mxdObject.ActiveView.FocusMap

    activeView = mxdObject.ActiveView
    pageLayout = activeView.QueryInterface(esriCarto.IPageLayout)
    graphicsContainer = pageLayout.QueryInterface(esriCarto.IGraphicsContainer)

    frameElement = graphicsContainer.FindFrame(IMap)
    mapFrame = frameElement.QueryInterface(esriCarto.IMapFrame)

    mapGrids = mapFrame.QueryInterface(esriCarto.IMapGrids)
    grids = [mapGrids.MapGrid(i) for i in xrange(mapGrids.MapGridCount)]
    grids_names = [int(i.Name) for i in grids]
    grids_names.sort()
    idx = bisect(grids_names, scale) - 1

    selected_grid = str(grids_names[idx])
    
    for g in grids:
        if g.Name == selected_grid:
            g.Visible = True
        else:
            g.Visible = False
    mxdObject.Save()
    del mxdObject


# params = {'Units': 9, 'UnitLabel': 'Meters', 'Division': 850}
# set_text_element_popup_properties(r'C:\assets\temp\26f1279bf0db4a71831727ee2b7ec0d8\LoremIpsum.mxd', 'FIGURAPOPUP', **params)
# select_grid_by_scale(r'c:\users\proyec~1\appdata\local\temp\MXD_3795e7b33b794e86a0b0f93a8b888ad3\MXD_3795e7b33b794e86a0b0f93a8b888ad3.mxd', 100000)